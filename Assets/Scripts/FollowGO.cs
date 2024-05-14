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
        if (objetivo != null && !estaColisionando)
        {
            // Interpolar la posicion para seguir al objetivo
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

        /*No funciona porque el Adventurer puede entrar pero no salir
        if (other.CompareTag("Wall"))
        {
            this.estaColisionando = true;
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lamp"))
        {
            this.estaColisionando = false;
        }

        /*No funciona porque el Adventurer puede entrar pero no salir
        if (other.CompareTag("Wall"))
        {
            this.estaColisionando = false;
        }*/
    }
}

