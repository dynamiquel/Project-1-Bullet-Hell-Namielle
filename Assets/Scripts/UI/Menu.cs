using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource fxAudioSource;
    public bool isController;
    public List<UserInput> userInputButtons;
    public TextMeshProUGUI title;
    public TextMeshProUGUI subheading;
    public EventSystem eventSystem;

    private void OnEnable()
    {
        if (eventSystem)
        {
            eventSystem.enabled = true;
        }
    }

    private void LateUpdate()
    {
        UserInput();
        CheckIfControllerPluggedIn();
    }

    public void ButtonClick(int audioClip)
    {
        if (fxAudioSource)
            if (audioClips.Length > 0)
                if (audioClip < audioClips.Length)
                    fxAudioSource.PlayOneShot(audioClips[audioClip]);
    }

    public void UserInputButtonClicked(int x)
    {

    }

    void EnableUserInputButtons(bool state)
    {
        if (userInputButtons.Count > 0)
            foreach (var item in userInputButtons)
                item.gameObject.SetActive(state);
    }

    public virtual void UserInput()
    {

    }

    void CheckIfControllerPluggedIn()
    {
        string[] joysticks = Input.GetJoystickNames();

        if (joysticks.Any(x => x.Length != 0))
        {
            isController = true;
            ChangeUserInputButtonsState(true);
        }
        else
        {
            isController = false;
            ChangeUserInputButtonsState(false);
        }
    }

    void ChangeUserInputButtonsState(bool state)
    {
        foreach (var item in userInputButtons)
        {
            item.EnableController(state);
        }
    }
}
