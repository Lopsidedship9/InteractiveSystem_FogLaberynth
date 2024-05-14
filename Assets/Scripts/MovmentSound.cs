using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentSound : MonoBehaviour
{
    public AudioSource walkSound;  
 
    private void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
            if (walkSound.isPlaying)
            {
                walkSound.Stop();
            }
        }
    }
}
