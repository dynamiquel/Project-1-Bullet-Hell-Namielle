using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(BoxCollider2D))]
public class LevelSlot : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string levelDataId;
    [SerializeField] TextMeshProUGUI perkNameText;
    [SerializeField] Color32 unlockedColour = new Color32(66, 245, 123, 255);
    [SerializeField] Color32 lockedColour = new Color32(230, 189, 9, 255);
    [SerializeField] Color32 outOfReachColour = new Color32(214, 92, 4, 128);
    LevelMenu levelMenu;
    LevelSelectData levelData;
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
        //perkMenu.DisplayPerkTooltip(perkData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //perkMenu.DisplayPerkTooltip(perkData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //perkMenu.HidePerkTooltip();
    }

    public void Click()
    {
        // if not already unlocked.
        levelMenu.PlayLevel(this);
    }

    public void Setup(string levelId, LevelMenu levelMenu)
    {
        levelDataId = levelId;
        this.levelMenu = levelMenu;
        SetContent();
    }

    public void SetContent()
    {
        levelData = ItemDatabase.Instance.LevelDatas[levelDataId];
        perkNameText.text = levelData.Name;
        // if unlocked, set text to unlockedColour
        // if cant afford, outOfReachColour
    }
}
