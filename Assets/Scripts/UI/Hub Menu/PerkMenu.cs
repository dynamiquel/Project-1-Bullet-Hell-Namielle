using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMenu : Menu
{
    public PerkController PerkController
    {
        get => LevelController.Instance.PlayerController.PerkController;
    }

    [SerializeField] GameObject perkToolTipPrefab;
    [SerializeField] GameObject perkSlotPrefab;
    [SerializeField] Transform perkGrid;
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
        foreach (var slot in perkSlots)
            slot.SetContent();

        if (perkSlots.Count > 0)
            GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = perkSlots[0].gameObject;
    }
}
