using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UserInput : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI textLabel;
    [SerializeField]
    string text;
    [SerializeField]
    XboxButton _button;
    public XboxButton Button
    {
        get { return _button; }
        set
        {
            _button = value;
            ButtonChanged();
        }
    }

    public void Start()
    {
        textLabel.text = text;
        ButtonChanged();
    }

    public void EnableController(bool state)
    {
        image.enabled = state;
    }

    void ButtonChanged()
    {
        image.sprite = Resources.Load<Sprite>($"Xbox One/XboxOne_{_button.ToString()}");
    }
}

public enum XboxButton
{
    A,
    B,
    X,
    Y,
    Up,
    Down,
    Left,
    Right,
    LB,
    RB,
    LT,
    RT,
    LS,
    RS,
    Menu,
    View
}