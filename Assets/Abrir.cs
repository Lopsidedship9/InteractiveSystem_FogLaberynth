using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir : MonoBehaviour
{
   private button1 button1; // Reference to the Lever script
   private button2 button2; // Reference to the Lever script
    private bool isMoved = false; // Track if the object has already been moved
    private bool both = false; 


    // Start is called before the first frame update
    void Start()
    {
        button1 = FindObjectOfType<button1>(); // Find the Lever script in the scene
        button2 = FindObjectOfType<button2>(); // Find the Lever script in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (button1 != null && button2 != null ) // Ensure lever reference is not null
        {
            if (button1.palanca1 + button2.palanca2 == 1 && !isMoved && !both) // If lever is activated and object is not moved yet
            {
                transform.position += Vector3.right * 2f; // Move the object forward along the z-axis by 10 units
                isMoved = true; // Set isMoved to true to indicate the object has been moved
            }
            else if (button1.palanca1+button2.palanca2 == 0 && isMoved && !both) // If lever is deactivated and object is moved
            {
                transform.position += Vector3.left * 2f; // Move the object backward along the z-axis by 10 units
                isMoved = false; // Set isMoved to false to indicate the object has returned to its original position
            }
            else if (button1.palanca1+button2.palanca2 == 2 && !both) // If lever is deactivated and object is moved
            {
                transform.position += Vector3.forward * 9f; // Move the object backward along the z-axis by 10 units
                both = true; // Set isMoved to false to indicate the object has returned to its original position
            }
        }
    }
}
