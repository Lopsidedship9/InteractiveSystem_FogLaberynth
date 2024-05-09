using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGO : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 1.0f;
    private bool estaColisionando = false;

    void Update()
    {
        // Si hay un objetivo
        if (objetivo != null && !estaColisionando)
        {
            // Interpolar la posición para seguir al objetivo
            Vector3 nuevaPosicion = Vector3.Lerp(transform.position, new Vector3(objetivo.position.x, transform.position.y, objetivo.position.z), velocidad * Time.deltaTime);
            transform.position = nuevaPosicion;
        }
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

