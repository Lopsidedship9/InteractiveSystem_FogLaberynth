using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGO : MonoBehaviour
{
    public Transform objetivo; // El objeto a seguir
    public float velocidad = 1.0f; // Velocidad de seguimiento

    void Update()
    {
        // Si hay un objetivo
        if (objetivo != null)
        {
            // Interpolar la posición para seguir al objetivo
            Vector3 nuevaPosicion = Vector3.Lerp(transform.position, objetivo.position, velocidad * Time.deltaTime);
            transform.position = nuevaPosicion;
        }
    }
}
