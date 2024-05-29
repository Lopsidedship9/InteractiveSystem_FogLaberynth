using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearWoodMovement : MonoBehaviour
{
    public GameObject wood;
    public GameObject wall;
    
    public void DestroyWood()
    {
        if (wood != null)
        {
            Destroy(wood, 1.0f);
        }
        if (wall != null)
        {
            Destroy(wall, 1.0f);
        }
    }
}
