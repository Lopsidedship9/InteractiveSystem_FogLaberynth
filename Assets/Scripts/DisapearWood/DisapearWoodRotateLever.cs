using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisapearWoodRotateLever : MonoBehaviour
{
    private bool isRotated = false; //Dira si ya ha rotado o si esta en la posicion inicial
    public float TEspera = 1.0f;
    //private Estado needRotation = Estado.Stop;
    private float targetRotation = 0f; //Inicialiaz la variable donde guardaremos la posicion objetivo
    private bool isRotating = false; // Controla si esta rotando para no hacer nada mas
    private float initialRotation;
    private float rotationTimeElapsed;
    private bool oneRotation = false;
    private bool noMore = false; //Por si queremos hacer que solo se levante una vez
    public DisapearWoodMovement DisapearWoodMovementScript;
    public SoundManager soundManager;
    void Start()
    {
        targetRotation = transform.eulerAngles.x; // Inicializa la rotacion objetivo
    }

    void Update()
    {
        if (isRotating && noMore == false)
        {
            
            rotationTimeElapsed += Time.deltaTime;
            float rotationFraction = rotationTimeElapsed / TEspera; //Calcula las particiones para el movimiento
            float newRotation = Mathf.LerpAngle(initialRotation, targetRotation, rotationFraction);
            transform.eulerAngles = new Vector3(newRotation, transform.eulerAngles.y, transform.eulerAngles.z);

            if (rotationTimeElapsed >= TEspera) 
            {
                if (isRotated == true)//Sabemos que si esta arriba la palanca el muro esta arriba tb.
                {
                    soundManager.PlayExplosionWoodSound();
                    DisapearWoodMovementScript.DestroyWood();
                }
                else
                {
                    
                }
                isRotating = false;
                //needRotation = Estado.Stop;
                rotationTimeElapsed = 0f;
                //soundManager.StopLeverSound();
                if(oneRotation)
                {
                    noMore = true;
                }
            }
        }
    }

    
    public void StartRotation()
    {
        if (!isRotating)
        {
            soundManager.PlayLeverSound();
            initialRotation = transform.eulerAngles.x;
            if (isRotated)
            {
                //needRotation = Estado.NegativeR;
                targetRotation = initialRotation - 90f;
            }
            else
            {
                //needRotation = Estado.PositiveR;
                targetRotation = initialRotation + 90f;
            }
            isRotated = !isRotated;
            isRotating = true;
            rotationTimeElapsed = 0f; // Resetea el tiempo de rotacion
        }
    }
}

