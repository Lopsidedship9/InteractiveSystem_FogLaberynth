using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public RotateLever rotateLeverScript;// Referencia al script RotateLever del hijo

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Detected Player");
            rotateLeverScript.StartRotation();
        }
    }
}
