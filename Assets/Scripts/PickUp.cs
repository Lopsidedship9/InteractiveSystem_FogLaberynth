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
    }

    private void RecogerObjeto(GameObject objeto)
    {
        // Asigna el objeto recogido como hijo del recogedor
        objeto.transform.SetParent(transform);
        
        // Opcionalmente, desactiva la física del objeto recogido
        Rigidbody rb = objeto.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Coloca el objeto recogido en la misma posición que el recogedor
        objeto.transform.localPosition = Vector3.zero;

        // Guarda la referencia al objeto recogido
        objetoRecogido = objeto;
    }

    private void SoltarObjeto()
    {
        // Verifica que hay un objeto recogido para soltar
        if (objetoRecogido != null)
        {
            // Elimina el objeto recogido como hijo del recogedor
            objetoRecogido.transform.SetParent(null);

            // Vuelve a habilitar la física del objeto recogido
            Rigidbody rb = objetoRecogido.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // Limpia la referencia al objeto recogido
            objetoRecogido = null;

            // Inicia el tiempo de espera antes de volver a recoger
            TRestante = TEspera;
        }
    }
    
    
    
}



