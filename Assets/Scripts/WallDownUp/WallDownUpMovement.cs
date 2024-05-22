using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDownUpMovement : MonoBehaviour
{
    public float speed = 1.0f;         // Velocidad
    public float lowerY = -4.0f;       // Limite inferior en Y
    public float upperY = 1.0f;        // Limite superior en Y
    private bool moveDown = false;      // Direcciones del movimiento
    private bool moveUp = false;
    private Vector3 newposDown;
    private Vector3 newposUp;
    private Vector3 pos;
    void Update()
    {
        pos = transform.position;
        if (moveDown)
        {
            newposDown = pos + (Vector3.down * speed * Time.deltaTime);
            if (newposDown.y <= lowerY)
            {
                Debug.Log("MaxDown");
                transform.position = new Vector3(pos.x, lowerY, pos.z);
                moveDown = false;
            }
            else
            {
                transform.position = newposDown;
            }
        }
        if(moveUp)
        {
            newposUp = pos + (Vector3.up * speed * Time.deltaTime);
            if (newposUp.y >= upperY)
            {
                Debug.Log("MaxUp");
                transform.position = new Vector3(pos.x, upperY, pos.z);
                moveUp = false;
            }
            else    
            {
                transform.position = newposUp;
            }
        }
    }

    public void goDown()
    {
        Debug.Log("Go Down");
        moveDown = true;
    }

    public void goUp()
    {
        Debug.Log("Go Up");
        moveUp = true;
    }
}

