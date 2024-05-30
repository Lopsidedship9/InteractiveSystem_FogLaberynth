using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public void setPosition(Vector3 pos)
    {
        Vector3 newPos;
        if(pos.y <= 0.6f)
        {
            newPos = Vector3.Lerp(transform.position, new Vector3(pos.x, 0.5f, pos.z), 0.05f);
        }
        else
        {
            newPos = Vector3.Lerp(transform.position, new Vector3(pos.x, 2.0f, pos.z), 0.05f);
        }
        transform.position = newPos;
    }

    public void setRotation(Quaternion quat)
    {
        Matrix4x4 mat = Matrix4x4.Rotate(quat);
        transform.localRotation = quat;
    }
}
