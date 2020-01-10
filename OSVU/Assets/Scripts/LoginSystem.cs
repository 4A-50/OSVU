using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LoginSystem : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;

    public GameObject error;

    public GameObject playVR;
    public GameObject startServer;

    public void buttonPress()
    {
        StartCoroutine(loginAccount());
    }

    IEnumerator loginAccount()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("uid=" + username.text + "&pwd=" + password.text));

        UnityWebRequest www = UnityWebRequest.Post("", form);
        yield return www;
        if (www.downloadHandler.text == "Login Success")
        {
            playVR.SetActive(true);
            startServer.SetActive(true);
        }
        else
        {
            error.SetActive(true);
        }
    }
}
