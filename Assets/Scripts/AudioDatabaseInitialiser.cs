using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDatabaseInitialiser : MonoBehaviour
{
    [SerializeField]
    List<SoundClip> soundClips = new List<SoundClip>();
    [SerializeField]
    bool destroyAfterInjection = true;

    public void Awake()
    {
        foreach (var item in soundClips)
        {
            if (!string.IsNullOrEmpty(item.id) || item.audioClip != null)
                AudioDatabase.AddClip(item);
            else
                Debug.LogWarning("Invalid Audio Clip addedto AudioDatabaseInitialiser");
        }

        if (destroyAfterInjection)
            Destroy(this);
    }
}

[System.Serializable]
public struct SoundClip
{
    public string id;
    public AudioClip audioClip;
}