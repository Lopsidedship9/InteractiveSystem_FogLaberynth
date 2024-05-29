using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearWoodDetectPlayer : MonoBehaviour
{
    public DisapearWoodRotateLever DisapearWoodRotateLeverScript;// Referencias al script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Detected Player");
            DisapearWoodRotateLeverScript.StartRotation();
        }
    }
}
