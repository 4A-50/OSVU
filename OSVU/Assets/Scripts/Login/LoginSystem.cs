using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Net;
using Valve.VR;

public class LoginSystem : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;

    public GameObject error;
    public GameObject gitHub;

    public UnityEngine.UI.Button playVR;
    public UnityEngine.UI.Button startServer;

    public SteamVR_LoadLevel levelLoader;

    void Start()
    {
        if (System.IO.File.Exists(@"buildIdentifier.txt"))
        {
            string[] build= System.IO.File.ReadAllLines(@"buildIdentifier.txt");

            string webResponse = new WebClient().DownloadString(build[2]);

            if (build[0] == build[1] && webResponse == build[0])
            {
                Globals.buildInfo = build;
                Globals.authed = true;
            }
            else
            {
                gitHub.SetActive(true);
            }
        }
        else
        {
            gitHub.SetActive(true);
        }
    }

    public void buttonPress()
    {
        StartCoroutine(loginAccount());
    }

    IEnumerator loginAccount()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("uid=" + username.text + "&pwd=" + password.text));

        if (Globals.authed == true)
        {
            UnityWebRequest www = UnityWebRequest.Post(Globals.buildInfo[3], form);

            yield return www;
            if (www.downloadHandler.text == "Login Success")
            {
                Globals.userName = username.text;
                playVR.interactable = true;
                startServer.interactable = true;
            }
            else
            {
                error.SetActive(true);
            }
        }
    }

    public void playInVR()
    {
        levelLoader.Trigger();
    }
}
