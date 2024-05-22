using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDownUpDetectPlayer : MonoBehaviour
{
    public WallDownUpRotateLever WallDownUpRotateLeverScript;// Referencias al script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Detected Player");
            WallDownUpRotateLeverScript.StartRotation();
        }
    }
}
