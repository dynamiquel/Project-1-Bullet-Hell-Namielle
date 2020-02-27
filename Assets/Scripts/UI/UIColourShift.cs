using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIColourShift : MonoBehaviour
{
    [SerializeField] float shiftTime = 0.5f;
    [SerializeField] Color32 rChannel = new Color32(255, 0, 0, 125);
    [SerializeField] Color32 gChannel = new Color32(0, 255, 0, 125);
    [SerializeField] Color32 bChannel = new Color32(0, 0, 255, 125);
    [SerializeField] bool withGarbageCollection = false; // Enabled: Creates/Destroys the objects. Disabled: Enables/Disables the objects. I assume Disabled would be better for something like this due to garbage collection but not sure.
    bool initalisedOnce = false;

    [SerializeField] List<TextMeshProUGUI> textElements = new List<TextMeshProUGUI>();
    [SerializeField] List<Image> imageElements = new List<Image>();
    [SerializeField] GameObject colourShiftTextPrefab;
    [SerializeField] GameObject colourShiftImagePrefab;

    List<ShiftedTextGroup> shiftedTextGroups = new List<ShiftedTextGroup>();
    List<ShiftedImageGroup> shiftedImageGroups = new List<ShiftedImageGroup>();

    public void StartShift()
    {
        if (gameObject.activeSelf)
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
                shiftedTextGroups.Add(CreateShiftedTextElements(textElement));
        }

        foreach (var imageElement in imageElements)
            if (!initalisedOnce || withGarbageCollection)
                shiftedImageGroups.Add(CreateShiftedImageElements(imageElement));

        initalisedOnce = true;
    }

    void EnableShiftedGroups()
    {
        foreach (var shiftedGroup in shiftedTextGroups)
        {
            shiftedGroup.Enable();
        }

        foreach (var shiftedGroup in shiftedImageGroups)
        {
            shiftedGroup.Enable();
        }
    }

    void DisableShiftedGroups()
    {
        foreach (var shiftedGroup in shiftedTextGroups)
        {
            if (withGarbageCollection)
                Destroy(shiftedGroup.gameObject);
            else
                shiftedGroup.Enable(false);
        }

        if (withGarbageCollection)
            shiftedTextGroups.Clear();

        foreach (var shiftedGroup in shiftedImageGroups)
        {
            if (withGarbageCollection)
                Destroy(shiftedGroup.gameObject);
            else
                shiftedGroup.Enable(false);
        }

        if (withGarbageCollection)
            shiftedImageGroups.Clear();
    }

    ShiftedTextGroup CreateShiftedTextElements(TextMeshProUGUI textElement)
    {
        ShiftedTextGroup textGroup = Instantiate(colourShiftTextPrefab, textElement.transform.parent).GetComponent<ShiftedTextGroup>();
        textGroup.Set(textElement, rChannel, gChannel, bChannel);
        return textGroup;
    }

    ShiftedImageGroup CreateShiftedImageElements(Image imageElement)
    {
        ShiftedImageGroup imageGroup = Instantiate(colourShiftImagePrefab, imageElement.transform.parent).GetComponent<ShiftedImageGroup>();
        imageGroup.Set(imageElement, rChannel, gChannel, bChannel);
        return imageGroup;
    }
}