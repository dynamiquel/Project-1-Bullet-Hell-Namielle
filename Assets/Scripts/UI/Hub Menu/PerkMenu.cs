using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerkMenu : Menu
{
    public PerkController PerkController
    {
        get => LevelController.Instance.PlayerController.PerkController;
    }
    public int PerkPoints
    {
        get => LevelController.Instance.PlayerController.stats.PersistentPlayerData.PerkPoints;
        private set => LevelController.Instance.PlayerController.stats.PersistentPlayerData.PerkPoints = value;
    }

    [SerializeField] GameObject perkToolTipPrefab;
    [SerializeField] GameObject perkSlotPrefab;
    [SerializeField] Transform perkGrid;
    [SerializeField] TextMeshProUGUI perkPointsText;
    PerkTooltip perkTooltip;

    PerkData perkData = null;
    List<PerkSlot> perkSlots = new List<PerkSlot>();

    private void Start()
    {
        CreatePerkSlots();
        CreatePerkTooltip();
    }

    private void OnGUI()
    {
        // Will improve. 
        if (perkData != null)
        {
            Vector2 position;
            if (!isController)
                position = Input.mousePosition;
            else // Eh, it'll do I guess.
                position = new Vector2(0, 0);

            perkTooltip.SetPosition(position.x, position.y, true);
        }
        else
            perkTooltip.Clear();
    }

    private void OnEnable()
    {
        RefreshPerkSlots();
    }

    public void DisplayPerkTooltip(PerkData perkData)
    {
        perkTooltip.SetContent(perkData.Name, perkData.Description, $"Cost: {perkData.Cost.ToString()}");
        this.perkData = perkData;
    }

    public void HidePerkTooltip()
    {
        perkData = null;
    }

    public void BuyPerk(PerkSlot perkSlot)
    {
        PlayerController playerController = LevelController.Instance.PlayerController;

        if (playerController.PerkController.ActivePerks.ContainsKey(perkSlot.perkDataId))
        {
            Debug.Log(string.Format("Player has already unlocked perk: {0}", perkSlot.perkDataId));
        }
        else
        {
            // If the player can afford the perk..
            if (PerkPoints >= ItemDatabase.Instance.PerkDatas[perkSlot.perkDataId].Cost)
            {
                // Add the perk to the player.
                playerController.PerkController.AddPerk(perkSlot.perkDataId);
                // Pay for the perk.
                PerkPoints -= ItemDatabase.Instance.PerkDatas[perkSlot.perkDataId].Cost;

                Debug.Log(string.Format("Player has unlocked the perk: {0}", perkSlot.perkDataId));
            }
            else
                Debug.Log(string.Format("Player cannot afford the perk: {0}", perkSlot.perkDataId));
        }

        RefreshPerkSlots();
    }

    void CreatePerkTooltip()
    {
        perkTooltip = Instantiate(perkToolTipPrefab, transform).GetComponent<PerkTooltip>();
        perkToolTipPrefab = null;
    }

    void CreatePerkSlots()
    {
        foreach (var perk in ItemDatabase.Instance.PerkDatas)
        {
            PerkSlot perkSlot = Instantiate(perkSlotPrefab, perkGrid).GetComponent<PerkSlot>();
            perkSlot.Setup(perk.Key, this);
            perkSlots.Add(perkSlot);
        }
    }

    void RefreshPerkSlots()
    {
        perkPointsText.text = string.Format("Perk Points: {0}", PerkPoints);

        foreach (var slot in perkSlots)
            slot.SetContent();

        if (perkSlots.Count > 0)
            GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = perkSlots[0].gameObject;
    }
}
