using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir : MonoBehaviour
{
    public ButtonLeverActivation button1; // Reference to the Lever script
    public ButtonLeverActivation button2; // Reference to the Lever script
    private bool isMoved = false; // Track if the object has already been moved
    private bool both = false;
    private Vector3 targetPosition; // Target position for smooth movement
    public float movementSpeed = 2f; // Speed of movement
    public GameObject decal; 

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position; // Set initial target position to current position
    }

    // Update is called once per frame
    void Update()
    {
        if (button1 != null && button2 != null) // Ensure lever reference is not null
        {
            // Smooth movement towards target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);

            // Check lever states and update target position accordingly
            if (((!button1.active && button2.active) || (button1.active && !button2.active)) && !isMoved && !both)
            {
                targetPosition += Vector3.right * 2f; // Move the object forward along the x-axis by 2 units
                isMoved = true; // Set isMoved to true to indicate the object has been moved
            }
            else if ((!button1.active && !button2.active) && isMoved && !both)
            {
                targetPosition += Vector3.left * 2f; // Move the object backward along the x-axis by 2 units
                isMoved = false; // Set isMoved to false to indicate the object has returned to its original position
            }
            else if ((button1.active && button2.active) && !both)
            {
                targetPosition += Vector3.right * 9f; // Move the object forward along the z-axis by 9 units
                both = true; // Set both to true to indicate the object has moved forward
                if (decal != null)
                {
                    Destroy(decal);
                }
            }
        }
    }
}
