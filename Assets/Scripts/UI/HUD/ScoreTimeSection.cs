using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using System;

// Move all time shit to player/level controller or something. This should be UI only.
public class ScoreTimeSection : HUDComponent
{
    [SerializeField]
    TextMeshProUGUI scoreValueText;
    [SerializeField]
    TextMeshProUGUI timeValueText;

    private void Start()
    {
        LevelController.Instance.OnScoreChanged += HandleScoreChange;
    }

    private void OnGUI()
    {
        if (timeValueText != null)
        {
            var t = TimeSpan.FromMilliseconds(LevelController.Instance.PlayerController.stats.PlayTime);
            timeValueText.text = t.ToString(@"mm\:ss\:f");
        }
    }

    void HandleScoreChange(long newScore)
    {
        if (scoreValueText != null)
            scoreValueText.text = newScore.ToString();
    }
}
