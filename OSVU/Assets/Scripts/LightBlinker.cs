using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class LightBlinker : MonoBehaviour
{
    Light lightToBlink;

    public float blinkSpeed = 0.5f;
    public bool blinking = true;

    void Start()
    {
        lightToBlink = GetComponent<Light>();
        StartCoroutine(blinker());
    }

    IEnumerator blinker()
    {
        while(blinking == true)
        {
            lightToBlink.enabled = true;
            yield return new WaitForSeconds(blinkSpeed);
            lightToBlink.enabled = false;
            yield return new WaitForSeconds(blinkSpeed);
        }
       
    }
}
