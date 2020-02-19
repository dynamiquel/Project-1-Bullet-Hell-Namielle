using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMenu : Menu
{
    [SerializeField] GameObject perkToolTipPrefab;
    [SerializeField] GameObject perkSlotPrefab;
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
            Vector2 v2 = Input.mousePosition;
            perkTooltip.SetPosition(v2.x, v2.y, true);
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

    void CreatePerkTooltip()
    {
        perkTooltip = Instantiate(perkToolTipPrefab, transform).GetComponent<PerkTooltip>();
        perkToolTipPrefab = null;
    }

    void CreatePerkSlots()
    {
        foreach (var perk in ItemDatabase.Instance.PerkDatas)
        {
            PerkSlot perkSlot = Instantiate(perkSlotPrefab, transform).GetComponent<PerkSlot>();
            perkSlot.Setup(perk.Key, this);
            perkSlots.Add(perkSlot);
        }
    }

    void RefreshPerkSlots()
    {
        foreach (var slot in perkSlots)
            slot.SetContent();
    }
}
