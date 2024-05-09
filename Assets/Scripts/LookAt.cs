using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Calcula la dirección desde el GameObject actual hacia el objetivo
            Vector3 directionToTarget = target.position - transform.position;

            // Bloqueamos la dirrecion Y para que no rote en esta (quedaria como si estuviera tumbado)
            directionToTarget.y = 0;
        
            directionToTarget.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

            // Aplica la rotación al GameObject
            transform.rotation = targetRotation;
        }  
    }
}
