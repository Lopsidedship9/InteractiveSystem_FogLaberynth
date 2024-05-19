using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Referencia al GameObject recogido actualmente
    private GameObject objetoRecogido = null;
    public KeyCode keyDrop = KeyCode.E;
    public float TEspera = 1.0f;
    private float TRestante = 0f;
    

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisionó es un objeto recogible y no hay ningún objeto ya recogido
        if (other.gameObject.CompareTag("Lamp") && objetoRecogido == null && TRestante <= 0)
        {
            RecogerObjeto(other.gameObject);
        }
    }

    void Update()
    {
        // Verifica si el usuario quiere soltar el objeto
        if (Input.GetKeyDown(keyDrop) && objetoRecogido != null)
        {
            SoltarObjeto();
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

        objetoRecogido.transform.position = new Vector3(objetoRecogido.transform.position.x, objetoRecogido.transform.position.y + 8, objetoRecogido.transform.position.z);
    }

    private void SoltarObjeto()
    {
        // Verifica que hay un objeto recogido para soltar
        if (objetoRecogido != null)
        {

            objetoRecogido.transform.position = new Vector3(objetoRecogido.transform.position.x, objetoRecogido.transform.position.y - 8, objetoRecogido.transform.position.z);

            // Limpia la referencia al objeto recogido
            objetoRecogido = null;

            // Inicia el tiempo de espera antes de volver a recoger
            TRestante = TEspera;
        }
    }

    private bool CheckCollision(Vector3 displacement)
    {

        Vector3 positionToCompare = transform.position + displacement;

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
}



