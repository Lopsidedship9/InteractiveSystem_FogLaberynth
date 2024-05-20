using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Referencia al GameObject recogido actualmente
    private GameObject objetoRecogido = null;
    public KeyCode keyDrop = KeyCode.E;
    public float TEspera = 0.3f;
    private float TRestante = 0f;
    private bool UpAWall = false;
    private bool objectPicked = false;
    
    //Solve problem with accuracy it should feel fluent and when it seams you can pick it up to always  work
    void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto con el que colisionó es un objeto recogible y no hay ningún objeto ya recogido
        if (other.gameObject.CompareTag("Lamp") && objetoRecogido == null && TRestante <= 0 && Input.GetKeyDown(keyDrop) && objectPicked == false)
        {
            RecogerObjeto(other.gameObject);
            objectPicked = true;
        }
    }

    void Update()
    {
        // Verifica si el usuario quiere soltar el objeto
        if (Input.GetKeyDown(keyDrop) && objetoRecogido != null && objectPicked == true && TRestante <= 0)
        {
            SoltarObjeto();
            objectPicked = false;
        }

        // Actualiza el tiempo de espera
        if (TRestante > 0)
        {
            TRestante -= Time.deltaTime;
        }

        if (objetoRecogido != null)
        {
            // Position the object in front of the player or at a designated position
            Vector3 newPosition = new Vector3(transform.position.x, objetoRecogido.transform.position.y, transform.position.z);
            objetoRecogido.transform.position = newPosition;
        }
    }


    private void RecogerObjeto(GameObject objeto)
    {


        // Guarda la referencia al objeto recogido
        objetoRecogido = objeto;
        Vector3 targetPosition;
        if (UpAWall)
        {
            targetPosition = objetoRecogido.transform.position + Vector3.up * 5;
            UpAWall = false;
        }
        else
        {
            targetPosition = objetoRecogido.transform.position + Vector3.up * 8;
        }

        // Start a coroutine to move the object smoothly to the target position
        StartCoroutine(MoveObject(objetoRecogido.transform, targetPosition, 0.3f));

        // Inicia el tiempo de espera antes de volver a recoger/soltar
        TRestante = TEspera;
    }

    private void SoltarObjeto()
    {
        // Verifica que hay un objeto recogido para soltar
        if (objetoRecogido != null)
        {
            Vector3 targetPosition;
            if (checkIfFallsToWall())
            {
                targetPosition = objetoRecogido.transform.position + Vector3.down * 5;
                UpAWall = true;
            }
            else
            {
                targetPosition = objetoRecogido.transform.position + Vector3.down * 8;
            }

            // Start a coroutine to move the object smoothly to the target position
            StartCoroutine(MoveObject(objetoRecogido.transform, targetPosition, 0.3f));

            // Limpia la referencia al objeto recogido
            objetoRecogido = null;

            // Inicia el tiempo de espera antes de volver a recoger/soltar
            TRestante = TEspera;
        }
    }


    private bool checkIfFallsToWall()
    {
        // Cast a ray downward
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f))
        {
            // Check if the collider's gameObject has the "Wall" tag
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }

    // Coroutine to smoothly move the object to the target position over time
    private IEnumerator MoveObject(Transform objectTransform, Vector3 targetPosition, float duration)
    {
        float timeElapsed = 0f;
        Vector3 startingPosition = objectTransform.position;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);
            objectTransform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }

        // Ensure the object reaches the exact target position
        objectTransform.position = targetPosition;
    }
}



