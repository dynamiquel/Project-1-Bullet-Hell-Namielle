using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

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
    
    public IntEvent OnClickEvent;
    public int id = 0;

    public void Start()
    {
        textLabel.text = text;
        ButtonChanged();
    }

    public void EnableController(bool state)
    {
        image.enabled = state;
    }

    public void OnClick()
    {
        print("Click 1");
        OnClickEvent.Invoke(id);
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

[System.Serializable]
public class IntEvent : UnityEvent<int>
{
}