using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using System;

// Ask Liam for help.
public class DiscordManager : MonoBehaviour
{
    [SerializeField] long clientId = 679471264213106737;
    Discord.Discord discord; 

    private void Awake()
    {
        discord = new Discord.Discord(clientId, (ulong)Discord.CreateFlags.NoRequireDiscord);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Debug.LogWarning("Could not connect to Discord.");
        }
    }

    public void SetActivity(string state, string details, string largeImageKey = "default", string largeImageText = "", string smallImageKey = "", string smallImageText = "")
    {
        if (discord == null)
            return;

        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = state,
            Details = details,
            Timestamps =
            {
                Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            },
            Assets =
            {
                LargeImage = largeImageKey,
                LargeText = largeImageText,
                SmallImage = smallImageKey,
                SmallText = smallImageText
            }
        };

        activityManager.UpdateActivity(activity, result => print("Discord Activity Updated"));
    }
}
