using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PerkSlot : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string perkDataId;
    [SerializeField] TextMeshProUGUI perkNameText;
    PerkMenu perkMenu;
    PerkData perkData;

    private void Start()
    {
        SetContent();
    }

    public void OnSelect(BaseEventData eventData)
    {
        perkMenu.DisplayPerkTooltip(perkData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        perkMenu.DisplayPerkTooltip(perkData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        perkMenu.HidePerkTooltip();
    }

    public void Click()
    {
        // if not already unlocked.
        Buy();
    }

    void Buy()
    {
        // if have enough points.
        // add to player's perk list.
    }

    public void Setup(string perkId, PerkMenu perkMenu)
    {
        perkDataId = perkId;
        this.perkMenu = perkMenu;
        SetContent();
    }

    public void SetContent()
    {
        perkData = ItemDatabase.Instance.PerkDatas[perkDataId];
        perkNameText.text = perkData.Name;
        // if unlocked, set text to green.
        // if cant afford, set text to red.
        // else, grey.
    }
}
