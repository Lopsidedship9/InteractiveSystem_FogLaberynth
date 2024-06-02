using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeverActivation : MonoBehaviour
{
    public bool active = false;
    public SoundManager soundManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soundManager.PlayButtonSound();
            StartCoroutine(ChangeActiveState(true));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soundManager.PlayButtonSound();
            StartCoroutine(ChangeActiveState(false));
        }
    }

    IEnumerator ChangeActiveState(bool newState)
    {
        yield return new WaitForSeconds(0.7f);
        active = newState;
    }
}
