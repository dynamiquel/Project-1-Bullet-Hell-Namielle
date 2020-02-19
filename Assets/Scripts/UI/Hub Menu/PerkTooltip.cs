using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerkTooltip : MonoBehaviour
{
    public bool Show { set => gameObject.SetActive(value); }

    [SerializeField] TextMeshProUGUI titleLabel;
    [SerializeField] TextMeshProUGUI descriptionLabel;
    [SerializeField] TextMeshProUGUI costLabel;
    RectTransform rectTransform;

    private void Awake()
    {
        Clear();
        rectTransform = transform as RectTransform;
    }

    public void SetContent(string title, string description, string cost)
    {
        titleLabel.text = title;
        descriptionLabel.text = description;
        costLabel.text = cost;
        SetHeight();
        gameObject.SetActive(true);
    }

    public void SetPosition(float x, float y, bool relativeToMouse)
    {
        if (relativeToMouse)
        {
            x += (rectTransform.sizeDelta.x / 2) + 2;
            y += (rectTransform.sizeDelta.y / 2) + 2;
        }

        transform.position = new Vector3(x, y, transform.position.z);
    }

    // Could be improved. Maths isn't my strong suit - also doesn't consider line breaks.
    // Adjusts the height of the tooltip depending on the length of the text.
    public void SetHeight()
    {
        int descriptionCharLength = descriptionLabel.text.Length;
        int rows = Mathf.CeilToInt(descriptionCharLength / 24);
        int height = 140 + (rows * 35);

        Vector2 newSizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
        rectTransform.sizeDelta = newSizeDelta;
    }

    public void Clear()
    {
        titleLabel.text = string.Empty;
        descriptionLabel.text = string.Empty;
        costLabel.text = string.Empty;
        gameObject.SetActive(false);
    }
}
