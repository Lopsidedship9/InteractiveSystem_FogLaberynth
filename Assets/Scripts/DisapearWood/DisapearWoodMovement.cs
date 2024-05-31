using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearWoodMovement : MonoBehaviour
{
    public GameObject wood;
    
    public void DestroyWood()
    {
        if (wood != null)
        {
            StartCoroutine(FadeOutAndDestroy(wood, 1.0f));
        }
    }

    private IEnumerator FadeOutAndDestroy(GameObject obj, float duration)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            Color color = material.color;
            float startAlpha = color.a;

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float blend = Mathf.Clamp01(t / duration);
                color.a = Mathf.Lerp(startAlpha, 0, blend);
                material.color = color;
                yield return null;
            }

            color.a = 0;
            material.color = color;
        }

        Destroy(obj);
    }
}
