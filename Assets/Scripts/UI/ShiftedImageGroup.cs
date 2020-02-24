using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftedImageGroup : MonoBehaviour
{
    public Image Original { get; set; }
    public Image R { get; set; }
    public Image G { get; set; }
    public Image B { get; set; }

    public Color32 RChannel { get; set; }
    public Color32 GChannel { get; set; }
    public Color32 BChannel { get; set; }

    float originalOpacity = 1f;

    private void OnGUI()
    {
        // Ensures the text content is up to date.
        if (Original != null)
        {
            R.sprite = Original.sprite;
            G.sprite = Original.sprite;
            B.sprite = Original.sprite;
        }
    }

    public void Set(Image original, Color32 rChannel, Color32 gChannel, Color32 bChannel)
    {
        Original = original;

        R = Instantiate(Original, Original.transform.position, Original.transform.rotation, transform);
        G = Instantiate(Original, Original.transform.position, Original.transform.rotation, transform);
        B = Instantiate(Original, Original.transform.position, Original.transform.rotation, transform);

        RChannel = rChannel;
        GChannel = gChannel;
        BChannel = bChannel;

        originalOpacity = Original.color.a;

        SetColours();
        SetPosition();
    }

    public void Enable(bool state = true)
    {
        var originalColour = Original.color;
        originalColour.a = state ? 0f : originalOpacity;
        Original.color = originalColour;
        gameObject.SetActive(state);
    }

    void SetColours()
    {
        R.color = RChannel;
        G.color = GChannel;
        B.color = BChannel;
    }

    void SetPosition()
    {
        R.transform.Translate(5f, 2f, 0f);
        G.transform.Translate(0f, -1f, 0f);
        B.transform.Translate(-5f, 2f, 0f);
    }
}