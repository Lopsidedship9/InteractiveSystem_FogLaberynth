using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSound : MonoBehaviour
{
    public SoundManager soundManager;
    public float rotationThreshold = 0.5f;  // Umbral para detectar rotación significativa
    private bool sonidoReproduciendose = false;
    private Quaternion lastRotation;
    private Quaternion currentRotation;
    private float angleMoved;

    private void Start()
    {
        lastRotation = transform.rotation;
    }

    private void Update()
    {
        currentRotation = transform.rotation;
        angleMoved = Quaternion.Angle(lastRotation, currentRotation);
        if (angleMoved > rotationThreshold)
        {
            if (!sonidoReproduciendose)
            {
                soundManager.PlayLeverSound();
                sonidoReproduciendose = true;
            }
        }
        else
        {
            if (sonidoReproduciendose)
            {
                //soundManager.StopLeverSound();
                sonidoReproduciendose = false;
            }
        }
        lastRotation = currentRotation;
    }
}