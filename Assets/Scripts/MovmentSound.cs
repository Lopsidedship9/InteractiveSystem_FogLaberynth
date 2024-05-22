using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentSound : MonoBehaviour
{
    public AudioSource sound;
    public float movementThreshold = 0.5f;  // Umbral para detectar movimiento significativo
    private bool sonidoReproduciendose = false;
    private Vector3 lastPosition;
    private Vector3 currentPosition;
    private  float distanceMoved;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        currentPosition = transform.position;
        distanceMoved = Vector3.Distance(currentPosition, lastPosition);
        if (distanceMoved > movementThreshold)
        {
            if (!sonidoReproduciendose)
            {
                sound.Play();
                sonidoReproduciendose = true;
            }
        }
        else
        {
            if (sonidoReproduciendose)
            {
                sound.Stop();
                sonidoReproduciendose = false;
            }
        }
        lastPosition = currentPosition;
    }
}
