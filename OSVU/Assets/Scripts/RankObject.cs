using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankObject : MonoBehaviour
{
    public enum rankControl {PassThrough, Disable};
    rankControl ObjectModifier;

    public Collider collider;

    void Start()
    {
        if (Globals.osvuRank == "Creator")
        {
            switch (ObjectModifier)
            {
                case rankControl.PassThrough:
                    collider.isTrigger = true;
                    break;
                case rankControl.Disable:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
