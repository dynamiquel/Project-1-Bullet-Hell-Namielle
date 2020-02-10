using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class FillBar : MonoBehaviour
{
    Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    [SerializeField] TextMeshProUGUI textLabel;
    [SerializeField] FillBarTextMode textMode;

    public float Value
    {
        set
        {
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);

            if (textMode == FillBarTextMode.Percentage)
                textLabel.text = $"{value / slider.maxValue * 100}%";
            else if (textMode == FillBarTextMode.Integer)
                textLabel.text = value.ToString();
            else if (textMode == FillBarTextMode.Off)
                textLabel.text = string.Empty;
        }
    }

    public float MaxValue { set => slider.maxValue = value; }

    private void Awake()
    {
        slider = GetComponent<Slider>();
        Value = 0;
    }

    public void SetValues(float value, float maxValue = 1f)
    {
        MaxValue = maxValue;
        Value = value;
    }
}

public enum FillBarTextMode
{
    Off,
    Percentage,
    Integer
}