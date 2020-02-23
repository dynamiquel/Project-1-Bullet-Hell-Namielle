using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColourShift : MonoBehaviour
{
    [SerializeField] float shiftTime = 0.5f;
    [SerializeField] Color32 rChannel = new Color32(255, 0, 0, 125);
    [SerializeField] Color32 gChannel = new Color32(0, 255, 0, 125);
    [SerializeField] Color32 bChannel = new Color32(0, 0, 255, 125);

    [SerializeField] List<TextMeshProUGUI> textElements = new List<TextMeshProUGUI>();
    [SerializeField] GameObject colourShiftPrefab;

    List<ShiftedTextGroup> shiftedGroups = new List<ShiftedTextGroup>();

    public void StartShift()
    {
        StartCoroutine(Shift());
    }

    IEnumerator Shift()
    {
        foreach (var textElement in textElements)
        {
            // Sets the opacity of the original text to 0 so it can't be seen.
            var colour = textElement.color;
            colour.a = 0f;
            textElement.color = colour;

            shiftedGroups.Add(CreateShiftedElements(textElement));
        }

        yield return new WaitForSeconds(shiftTime);

        foreach (var shiftedGroup in shiftedGroups)
        {
            // Sets the opacity of the original text to full.
            var colour = shiftedGroup.Original.color;
            colour.a = 1f;
            shiftedGroup.Original.color = colour;

            Destroy(shiftedGroup.gameObject);           
        }

        shiftedGroups.Clear();
    }

    ShiftedTextGroup CreateShiftedElements(TextMeshProUGUI textElement)
    {
        ShiftedTextGroup textGroup = Instantiate(colourShiftPrefab, textElement.transform).GetComponent<ShiftedTextGroup>();
        textGroup.Set(textElement, rChannel, gChannel, bChannel);
        return textGroup;
    }
}