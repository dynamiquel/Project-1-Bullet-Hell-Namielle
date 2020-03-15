using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
// Place this script on any object you wish to play audio when it is destroyed.
public class DestroyAudio : MonoBehaviour
{
    [SerializeField] [HideInInspector]
    GameObject destroyAudioPlayer;
    [SerializeField] string audioClipId = "Enemy Death";
    [SerializeField] [Range(0, 1)]
    float volume = 1f;
    [SerializeField] float lifeTime = 3f;

    bool quitting = false;

    private void OnDestroy()
    {
        if (!quitting)
            Instantiate(destroyAudioPlayer).GetComponent<DestroyedAudioPlayer>().Init(audioClipId, lifeTime, volume);
    }

    private void OnLevelWasLoaded(int level)
    {
        quitting = true;
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }
}
