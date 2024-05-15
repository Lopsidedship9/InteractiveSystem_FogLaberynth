using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 0.5f;

    public void RotationToMove(Vector3 new_position)
    {
        if (target != null)
        {
            // Define a small threshold value
            float threshold = 0.0001f; // Adjust as needed based on your requirements

            // Calculate the difference between the positions
            Vector3 positionDifference = transform.position - new_position;

            Vector3 directionToTarget;
            // Check if the magnitude of the difference is within the threshold
            if (positionDifference.sqrMagnitude > threshold * threshold)
            {
                // Calcula la dirección desde el GameObject actual hacia el objetivo
                directionToTarget = new_position - transform.position;
            } 
            else
            {
                // Calcula la dirección desde el GameObject actual hacia el objetivo
                directionToTarget = target.position - transform.position;
            }

            // Bloqueamos la dirrecion Y para que no rote en esta (quedaria como si estuviera tumbado)
            directionToTarget.y = 0;

            directionToTarget.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

            // Slerp between the current rotation and the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public bool CorrectOrientation(Vector3 new_position)
    {

        // Get the current movement direction (normalized)
        Vector3 movementDirection = (new_position - transform.position).normalized;

        // Get the forward direction of the GameObject (normalized)
        Vector3 forwardDirection = transform.forward;

        // Calculate the dot product between the movement direction and forward direction
        float dotProduct = Vector3.Dot(movementDirection, forwardDirection);

        // Calculate the cosine of the angle threshold in radians
        float cosThreshold = Mathf.Cos(20 * Mathf.Deg2Rad);

        // Check if the dot product is greater than or equal to the cosine of the angle threshold
        return dotProduct >= cosThreshold;
    }
}
