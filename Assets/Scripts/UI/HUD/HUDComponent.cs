using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDComponent : MonoBehaviour
{
    [SerializeField]
    bool _visible;
    public bool Visible
    {
        get => _visible;
        set
        {
            _visible = value;
            OnVisibilityChange();
        }
    }

    [SerializeField]
    [Range(0, 1)]
    float _opacity;
    public float Opacity
    {
        get => _opacity;
        set
        {
            _opacity = value;
            OnOpacityChange();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnOpacityChange();
        OnVisibilityChange();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Visible)
            return;
    }

    void OnVisibilityChange()
    {
        try
        {
            foreach (var tranform in GetComponentsInChildren<Transform>())
            {
                transform.gameObject.SetActive(Visible);
            }
        }
        catch (System.Exception e)
        {
            
        }
    }

    void OnOpacityChange()
    {
        foreach (var image in GetComponentsInChildren<Image>())
        {
            Color previousColour = image.color;
            previousColour.a = Opacity;
            image.color = previousColour;
        }

        foreach (var text in GetComponentsInChildren<TextMeshProUGUI>())
        {
            Color previousColour = text.color;
            previousColour.a = Opacity;
            text.color = previousColour;
        }
    }
}
