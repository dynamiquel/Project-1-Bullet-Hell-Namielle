using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    Image image;
    TextMeshProUGUI text;
    
    public void EnableController(bool state)
    {
        image.enabled = state;
    }
}
