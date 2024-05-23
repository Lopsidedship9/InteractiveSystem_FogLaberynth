using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Quaternion q;
    public bool manual;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPosition(Vector3 pos)
    {
        Vector3 newPos;
        if(pos.y <= 0.6f)
        {
            newPos = new Vector3(pos.x,0.5f,pos.z);
        }
        else
        {
            newPos = new Vector3(pos.x,2.0f,pos.z);
        }
        transform.position = newPos;
    }

    public void setRotation(Quaternion quat)
    {
        Matrix4x4 mat = Matrix4x4.Rotate(quat);
        transform.localRotation = quat;
    }
}
