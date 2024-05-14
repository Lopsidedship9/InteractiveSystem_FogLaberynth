using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentSound : MonoBehaviour
{
    public AudioSource walkSound;  
    private bool sonidoReproduciendose = false;

    private void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            if (!sonidoReproduciendose)
            {
                walkSound.Play();
                sonidoReproduciendose = true;
            }
        }
        else
        {
            if (sonidoReproduciendose)
            {
                walkSound.Stop();
                sonidoReproduciendose = false;
            }
        }
    }
}
