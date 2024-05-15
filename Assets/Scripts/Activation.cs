using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    private Lever lever; // Reference to the Lever script
    private bool isMoved = false; // Track if the object has already been moved

    // Start is called before the first frame update
    void Start()
    {
        lever = FindObjectOfType<Lever>(); // Find the Lever script in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (lever != null) // Ensure lever reference is not null
        {
            if (lever.palanca && !isMoved) // If lever is activated and object is not moved yet
            {
                transform.position += Vector3.forward * 9f; // Move the object forward along the z-axis by 10 units
                isMoved = true; // Set isMoved to true to indicate the object has been moved
            }
            else if (!lever.palanca && isMoved) // If lever is deactivated and object is moved
            {
                transform.position += Vector3.back * 9f; // Move the object backward along the z-axis by 10 units
                isMoved = false; // Set isMoved to false to indicate the object has returned to its original position
            }
        }
    }
}