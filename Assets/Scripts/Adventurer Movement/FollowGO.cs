using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FollowGO : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 1.0f;
    public GameObject adventurer_mesh;
    public GameObject lamp;
    public GameObject fog;
    public Rigidbody lampRigidbody;
    private bool estaColisionando = false;
    private LookAt LookAt;
    private Animator Animation;
    public SoundManager soundManager;
    private bool soundStopped;
    public ParticleSystemForceField forceField;
    private float targetForceFieldSize = 60.0f;
    public float expansionDuration = 3f;
    private bool music_end = false;
    public float moveDuration = 1.5f;
    private bool ChangeScene = true;
    public FinalScript FinalScript;

    void Start()
    {
        LookAt = adventurer_mesh.GetComponent<LookAt>();
        Animation = adventurer_mesh.GetComponent<Animator>();
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

            LookAt.RotationToMove(new_pos, Animation);
            if (LookAt.CorrectOrientation(new_pos) && LookAt.HasPositionChanged(new_pos))
            {
                transform.position = new_pos;
                if (Animation.GetBool("IsWalking") == false)
                {
                    Animation.SetBool("IsWalking", true);
                    soundManager.PlayWalkingSound();
                    soundStopped = false;
                }
            }
            else
            {
                if (Animation.GetBool("IsWalking") == true)
                {
                     Animation.SetBool("IsWalking", false);                        
                     soundManager.StopWalkingSound();
                }
            }
        }
        else
        {
            if(!soundStopped)
            {
                soundManager.StopWalkingSound();
                soundStopped = true;
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
            if (Animation.GetBool("IsWalking") == true)
            {
                Animation.SetBool("IsWalking", false);
            }
        }
        if (other.CompareTag("ChangeScene"))
        {
            //Debug.Log(forceField.endRange);
            StartCoroutine(ChangeForceFieldSize());                
        }
        if(other.CompareTag("FinalTrigger"))
        {
            ChangeScene = false;
            StartCoroutine(ChangeForceFieldSize());
            FinalScript.FinalAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lamp"))
        {
            this.estaColisionando = false;
        }
    }

    public IEnumerator ChangeForceFieldSize()
    {
        float startSize = forceField.endRange;
        float timeElapsed = 0;

        while (timeElapsed < expansionDuration)
        {
            forceField.endRange = Mathf.Lerp(startSize, targetForceFieldSize, timeElapsed / expansionDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // Espera hasta el siguiente frame
        }
        forceField.endRange = targetForceFieldSize;
        Destroy(fog);
        //Debug.Log(forceField.endRange);

        yield return StartCoroutine(LampDisapear());
        if(ChangeScene)
        {
            yield return StartCoroutine(Walk_out_animation());
        }
        else
        {
            yield return StartCoroutine(Walk_out_animation_Final());
        }
       
    }

    private IEnumerator Walk_out_animation()
    {
        Vector3 new_pos = transform.position;

        Vector3 direction = new Vector3(1.0f, 0f, 0f);
        Animation.SetBool("IsWalking", true);

        while (new_pos.x < 95.0f)
        {
            new_pos += direction * velocidad * Time.deltaTime;
            transform.position = new_pos;
            yield return null; // Espera hasta el siguiente frame
        }
        Animation.SetBool("IsWalking", false);

        Destroy(gameObject);
        SceneManager.LoadScene("Scene2");
    }

    private IEnumerator Walk_out_animation_Final()
    {
        Vector3 new_pos = transform.position;

        Vector3 direction = new Vector3(0f, 0f, -1.0f);
        Animation.SetBool("IsWalking", true);

        while (new_pos.z > 10.0f)
        {
            new_pos += direction * velocidad * Time.deltaTime;
            transform.position = new_pos;
            yield return null; // Espera hasta el siguiente frame
        }
        Animation.SetBool("IsWalking", false);
        Destroy(gameObject);

        //Destroy(gameObject);
        //SceneManager.LoadScene("Scene2");
        //Application.Quit(); Por si queremos hacer que se cierre el juego

    }

    public IEnumerator LampDisapear()
    {
        // Deactivate the lamp's Rigidbody component to allow it to move through walls
        lampRigidbody.isKinematic = true;

        Vector3 startPosition = lamp.transform.position; // Starting position of the lamp
        Vector3 targetPosition = new Vector3(lamp.transform.position.x, -3.0f, lamp.transform.position.z);
        float elapsedTime = 0f;

        // Loop until the elapsed time reaches the move duration
        while (elapsedTime < moveDuration)
        {
            // Calculate the interpolation factor based on the elapsed time and move duration
            float t = elapsedTime / moveDuration;

            // Interpolate the position between the starting position and the target position
            lamp.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Increment the elapsed time by the time since the last frame
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        Destroy(lamp);
    }

    
}

