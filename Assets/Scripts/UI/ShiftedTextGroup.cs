using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShiftedTextGroup : MonoBehaviour
{
    public TextMeshProUGUI Original { get; set; }
    public TextMeshProUGUI R { get; set; }
    public TextMeshProUGUI G { get; set; }
    public TextMeshProUGUI B { get; set; }

    public Color32 RChannel { get; set; }
    public Color32 GChannel { get; set; }
    public Color32 BChannel { get; set; }

    private void OnGUI()
    {
        // Ensures the text content is up to date.
        if (Original != null)
        {
            R.text = Original.text;
            G.text = Original.text;
            B.text = Original.text;
        }
    }

    public void Set(TextMeshProUGUI original, Color32 rChannel, Color32 gChannel, Color32 bChannel)
    {
        Original = original;

        R = Instantiate(Original, Original.transform.position, Original.transform.rotation, transform);
        G = Instantiate(Original, Original.transform.position, Original.transform.rotation, transform);
        B = Instantiate(Original, Original.transform.position, Original.transform.rotation, transform);

        RChannel = rChannel;
        GChannel = gChannel;
        BChannel = bChannel;

        SetColours();
        SetPosition();
    }

    void SetColours()
    {
        R.color = RChannel;
        G.color = GChannel;
        B.color = BChannel;
    }

    void SetPosition()
    {
        R.gameObject.transform.Translate(5f, 2f, 0f);
        G.gameObject.transform.Translate(0f, -1f, 0f);
        B.gameObject.transform.Translate(-5f, 2f, 0f);
    }
}