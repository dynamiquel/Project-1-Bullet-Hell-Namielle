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
    [SerializeField] bool withGarbageCollection = true; // Enabled: Creates/Destroys the objects. Disabled: Enables/Disables the objects. I assume Disabled would be better for something like this due to garbage collection but not sure.
    bool initalisedOnce = false;

    [SerializeField] List<TextMeshProUGUI> textElements = new List<TextMeshProUGUI>();
    [SerializeField] GameObject colourShiftPrefab;

    List<ShiftedTextGroup> shiftedGroups = new List<ShiftedTextGroup>();

    public void StartShift()
    {
        StartCoroutine(Shift());
    }

    IEnumerator Shift()
    {
        CreateShiftedGroups();

        EnableShiftedGroups();

        yield return new WaitForSeconds(shiftTime);

        DisableShiftedGroups();
    }

    void CreateShiftedGroups()
    {
        foreach (var textElement in textElements)
        {
            if (!initalisedOnce || withGarbageCollection)
                shiftedGroups.Add(CreateShiftedElements(textElement));
        }

        initalisedOnce = true;
    }

    void EnableShiftedGroups()
    {
        foreach (var shiftedGroup in shiftedGroups)
        {
            shiftedGroup.Enable();
        }
    }

    void DisableShiftedGroups()
    {
        foreach (var shiftedGroup in shiftedGroups)
        {
            if (withGarbageCollection)
                Destroy(shiftedGroup.gameObject);
            else
                shiftedGroup.Enable(false);
        }

        if (withGarbageCollection)
            shiftedGroups.Clear();
    }

    ShiftedTextGroup CreateShiftedElements(TextMeshProUGUI textElement)
    {
        ShiftedTextGroup textGroup = Instantiate(colourShiftPrefab, textElement.transform.parent).GetComponent<ShiftedTextGroup>();
        textGroup.Set(textElement, rChannel, gChannel, bChannel);
        return textGroup;
    }
}