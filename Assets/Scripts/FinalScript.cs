using System.Collections;
using UnityEngine;

public class FinalScript : MonoBehaviour
{
    public GameObject walls;
    public GameObject levers;
    public GameObject decals;
    public GameObject torches;  

    public AudioSource historySource;
    public AudioSource ambientSource;
    public AudioSource magicDisappearSource;
    public AudioSource despedidaSource;

    private bool isPlaying = false;
    private bool historyPlaing = false;
    private bool despedidaPlaing = false;

    void Start()
    {
        if (magicDisappearSource == null)
        {
            Debug.LogWarning("Magic Disappear AudioSource not assigned.");
        }

        if (historySource == null)
        {
            Debug.LogWarning("History AudioSource not assigned.");
        }

        if (ambientSource == null)
        {
            Debug.LogWarning("Ambient AudioSource not assigned.");
        }

        if (despedidaSource == null)
        {
            Debug.LogWarning("Despedida AudioSource not assigned.");
        }
    }

    public void FinalAnimation()
    {
        if (ambientSource != null)
        {
            ambientSource.Stop();
        }

        if (magicDisappearSource != null && magicDisappearSource.clip != null)
        {
            Invoke("PlaySound", 4.0f);
        }
        else
        {
            Debug.LogWarning("AudioSource o AudioClip no encontrado.");
        }
    }

    void PlaySound()
    {
        HideAllObjects();
        if (magicDisappearSource != null && magicDisappearSource.clip != null)
        {
            magicDisappearSource.Play();
            isPlaying = true;
            StartCoroutine(WaitForSoundToFinish());
        }
    }

    IEnumerator WaitForSoundToFinish()
    {
        while (isPlaying && magicDisappearSource.isPlaying)
        {
            yield return null;
        }

        if (historySource != null && historySource.clip != null)
        {
            historySource.Play();
            historyPlaing = true;
        }
        while (historyPlaing && historySource.isPlaying)
        {
            yield return null;
        }

        if (despedidaSource != null && despedidaSource.clip != null)
        {
            despedidaSource.Play();
            despedidaPlaing = true;
        }
        while (despedidaPlaing && despedidaSource.isPlaying)
        {
            yield return null;
        }
        //Para la build
        Application.Quit(); 
        // Para editor de Unity
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif

    }

    void Update()
    {
        if (isPlaying && !magicDisappearSource.isPlaying)
        {
            isPlaying = false;
        }

        if (historyPlaing && !historySource.isPlaying)
        {
            historyPlaing = false;
        }
        
        if (despedidaSource && !despedidaSource.isPlaying)
        {
            despedidaPlaing = false;
        }
    }

    public void HideAllObjects()
    {
        if (walls != null)
        {
            Destroy(walls);
        }
        if (levers != null)
        {
            Destroy(levers);
        }
        if (decals != null)
        {
            Destroy(decals);
        }
        if (torches != null)
        {
            Destroy(torches);
        }
    }
}
