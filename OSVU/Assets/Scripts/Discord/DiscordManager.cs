using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscordPresence;

public class DiscordManager : MonoBehaviour
{
    long currentTime;

    public string detail = "On Foot";
    public string state = "Space Station";

    void Start()
    {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        int secondsSinceEpoch = (int)t.TotalSeconds;
        currentTime = Convert.ToInt64(secondsSinceEpoch);

        PresenceManager.UpdatePresence(detail, state, currentTime, 0, "osvu_logo_lrg", "OSVU", null, null, null, 1, 16, "", null, "");
    }

    void Update()
    {
        if (detail != PresenceManager.instance.presence.details || state != PresenceManager.instance.presence.state)
        {
            PresenceManager.UpdatePresence(detail, state, currentTime, 0, "osvu_logo_lrg", "OSVU", null, null, null, 1, 16, "", null, "");
        }
    }
}