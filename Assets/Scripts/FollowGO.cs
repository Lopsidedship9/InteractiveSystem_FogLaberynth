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
            Vector3 nuevaPosicion = Vector3.Lerp(transform.position, new Vector3(objetivo.position.x, transform.position.y, objetivo.position.z), velocidad * Time.deltaTime);
            transform.position = nuevaPosicion;
        }
    }
}
