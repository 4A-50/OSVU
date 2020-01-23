using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json.Linq;
using DiscordPresence;

public class DiscordMainMenu : MonoBehaviour
{
    void Start()
    {
        var json = new WebClient().DownloadString("http://osvu.co.uk/api/public/getDRP.php");
        JObject objects = JObject.Parse(json);

        PresenceManager.UpdatePresence((string)objects.SelectToken("data.Details"), (string)objects.SelectToken("data.State"), 0, 0, "osvu_logo_lrg", "OSVU", null, null, null, 0, 0, "", null, "");
    }
}
