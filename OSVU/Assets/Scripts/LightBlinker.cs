using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class LightBlinker : MonoBehaviour
{
    Light lightToBlink;

    public float blinkSpeed = 0.5f;
    public bool blinking = true;

    public Renderer lightRend;

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
            if (lightRend != null)
            {
                lightRend.material.EnableKeyword("_EMISSION");
            }
            yield return new WaitForSeconds(blinkSpeed);
            lightToBlink.enabled = false;
            if (lightRend != null)
            {
                lightRend.material.DisableKeyword("_EMISSION");
            }
            yield return new WaitForSeconds(blinkSpeed);
        }
       
    }
}
