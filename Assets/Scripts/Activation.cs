using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    private Lever lever; // Reference to the Lever script
    private Vector3 originalPosition; // Store the original position of the object
    private Vector3 targetPosition; // Store the target position of the object
    private bool isMoving = false; // Track if the object is currently moving
    public float moveDuration = 2.0f; // Duration of the movement in seconds, adjusted to slow down the movement
    private Collider WallCollider;

    // Start is called before the first frame update
    void Start()
    {
        lever = FindObjectOfType<Lever>(); // Find the Lever script in the scene
        originalPosition = new Vector3(transform.position.x, 1f, transform.position.z); // Set the original y position to 1
        targetPosition = new Vector3(transform.position.x, -3f, transform.position.z); // Set the target y position to -3
        transform.position = originalPosition; // Ensure the wall starts at the original position
        WallCollider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lever != null) // Ensure lever reference is not null
        {
            if (lever.palanca && !isMoving && transform.position != targetPosition) // If lever is activated and object is not moving and not at target
            {
                StartCoroutine(MoveObject(originalPosition, targetPosition)); // Start moving the object to the target position
            }
            else if (!lever.palanca && !isMoving && transform.position != originalPosition) // If lever is deactivated and object is not moving and not at original
            {
                StartCoroutine(MoveObject(targetPosition, originalPosition)); // Start moving the object back to the original position
            }
        }
    }

    IEnumerator MoveObject(Vector3 start, Vector3 end)
    {
        isMoving = true; // Set isMoving to true to indicate the object is moving
        float elapsedTime = 0f; // Track the elapsed time

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / moveDuration); // Interpolate position
            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        transform.position = end; // Ensure the final position is set to the end position
        isMoving = false; // Set isMoving to false to indicate the object has finished moving

        if (transform.position == targetPosition)
        {
            WallCollider.enabled = false;
        } else
        {
            WallCollider.enabled = true;
        }
    }
}