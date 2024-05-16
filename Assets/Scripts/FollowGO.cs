using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGO : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 1.0f;
    public GameObject adventurer_mesh;
    private bool estaColisionando = false;
    private LookAt LookAt;

    void Start()
    {
        LookAt = adventurer_mesh.GetComponent<LookAt>();
    }
    void Update()
    {
        if (objetivo != null && !estaColisionando)
        {
            // Interpolate the position to follow the target
            Vector3 targetPosition = new Vector3(objetivo.position.x, transform.position.y, objetivo.position.z);

            //Use new_position to do modification before applying it to the real position
            Vector3 new_pos = transform.position;

            // Check for collisions separately in x and z directions
            Vector3 movementDirection = targetPosition - transform.position;
            Vector3 xMovement = new Vector3(movementDirection.x, 0f, 0f);
            if (movementDirection.x > 1.0f)
            {
                xMovement = new Vector3(1.0f, 0f, 0f);
            } 
            else if (movementDirection.x < -1.0f)
            {
                xMovement = new Vector3(-1.0f, 0f, 0f);
            }
            Vector3 zMovement = new Vector3(0f, 0f, movementDirection.z);
            if (movementDirection.z > 1.0f)
            {
                zMovement = new Vector3(0f, 0f, 1.0f);
            } 
            else if (movementDirection.z < -1.0f) 
            {
                zMovement = new Vector3(0f, 0f, -1.0f);
            }
            // Normalize movement direction to ensure consistent direction
            movementDirection.Normalize();

            if (!CheckCollision(movementDirection))
            {
                // If there's no collision in either direction, move in both x and z directions
                new_pos += movementDirection * velocidad * Time.deltaTime;
            }
            else if (!CheckCollision(xMovement))
            {
                // If there's a collision in the x direction, move only in the z direction
                new_pos += xMovement * velocidad * Time.deltaTime;
            }
            else if (!CheckCollision(zMovement))
            {
                // If there's a collision in the z direction, move only in the x direction
                new_pos += zMovement * velocidad * Time.deltaTime;
            }

            LookAt.RotationToMove(new_pos);
            if (LookAt.CorrectOrientation(new_pos))
            {
                transform.position = new_pos;
            }
        }
    }

    private bool CheckCollision(Vector3 displacement)
    {

        Vector3 positionToCompare = transform.position + displacement * velocidad * Time.deltaTime;

        // Define the tag of the GameObject you want to find
        string colliderTag = "Wall";

        Collider AdventurerCollider = this.GetComponent<Collider>();
        Bounds AdventurerBoundingBox = AdventurerCollider.bounds;
        AdventurerBoundingBox.center = positionToCompare;

        // Find all GameObjects with the specified tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(colliderTag);

        foreach (GameObject obj in taggedObjects)
        {
            // Check if the GameObject has a Collider component
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                Bounds expandedBounds = collider.bounds;

                // Check if the position is inside the expanded bounds
                if (expandedBounds.Intersects(AdventurerBoundingBox))
                {
                    return true; // There's a collision
                }
            }
        }

        return false; // No collision
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lamp"))
        {
            this.estaColisionando = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lamp"))
        {
            this.estaColisionando = false;
        }
    }
}

