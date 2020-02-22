using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(BoxCollider2D))]
public class PerkSlot : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string perkDataId;
    [SerializeField] TextMeshProUGUI perkNameText;
    [SerializeField] Color32 unlockedColour = new Color32(66, 245, 123, 255);
    [SerializeField] Color32 lockedColour = new Color32(230, 189, 9, 255);
    [SerializeField] Color32 outOfReachColour = new Color32(214, 92, 4, 128);
    PerkMenu perkMenu;
    PerkData perkData;
    Image fillImage;

    private void Awake()
    {
        fillImage = GetComponent<Image>();
    }

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
        perkMenu.BuyPerk(this);
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
        // if unlocked, set text to unlockedColour
        // if cant afford, outOfReachColour

        if (perkMenu.PerkController != null)
        {
            if (perkMenu.PerkController.ActivePerks.ContainsKey(perkDataId))
                fillImage.color = unlockedColour;
            else
            {
                if (perkMenu.PerkPoints >= perkData.Cost)
                    fillImage.color = lockedColour;
                else
                    fillImage.color = outOfReachColour;
            }
        }
    }
}
