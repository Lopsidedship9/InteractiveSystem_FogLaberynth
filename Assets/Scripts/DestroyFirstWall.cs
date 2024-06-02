using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFirstWall : MonoBehaviour
{
    public float delay = 31.0f;

    void Awake()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
