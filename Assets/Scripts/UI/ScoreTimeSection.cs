using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using System;

// Move all time shit to player/level controller or something. This should be UI only.
public class ScoreTimeSection : HUDComponent
{
    float _score = 0;
    public float Score
    {
        get => _score;
        set => _score += value;
    }

    long time = 0;

    [SerializeField]
    TextMeshProUGUI scoreValueText;
    [SerializeField]
    TextMeshProUGUI timeValueText;

    // Update is called once per frame
    void Update()
    {
        // Converts float to double for precision, then rounds back to float.
        time += Mathf.RoundToInt((float)((double)Time.deltaTime * 1000));
    }

    private void OnGUI()
    {
        if (scoreValueText != null)
            scoreValueText.text = Score.ToString();

        if (timeValueText != null)
        {
            var t = TimeSpan.FromMilliseconds(time);
            timeValueText.text = t.ToString(@"mm\:ss\:f");
        }
    }
}
