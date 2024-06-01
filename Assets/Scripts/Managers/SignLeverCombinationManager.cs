using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignLeverCombinationManager : MonoBehaviour
{
    public SignSequenceManager SignSequenceManager;
    private Dictionary<string, RotateLever> leverDictionary = new Dictionary<string, RotateLever>();
    private string[] sequence;
    private string[] sequenceProjectors;

    private bool notFinished = true;

    void Start()
    {
        // Get all RotateLever components and populate the dictionary
        RotateLever[] leverRotation = GetComponentsInChildren<RotateLever>();
        foreach (RotateLever leverScript in leverRotation)
        {
            leverDictionary.Add(leverScript.gameObject.name, leverScript);
        }

        // Define the sequence of lever names
        sequence = new string[] { "Swan Lever", "Vines Lever", "Fenix Lever", "Dragon Lever", "Circle Lever", "Tree Lever", "Two branches Lever", "Flowers Lever" };
        sequenceProjectors = new string[] { "Swan Decal Projector", "Vines Decal Projector", "Fenix Decal Projector", "Dragon Decal Projector", "Circle Decal Projector", "Tree Decal Projector" };
    }

    private void Update()
    {
        if (notFinished)
            UpdateLevers();
    }

    private void UpdateLevers()
    {
        bool correctSequence = true;
        RotateLever specificLever;
        int active = 0;
        for (int i = 0; i < sequence.Length - 2; i++)
        {
            if (leverDictionary.TryGetValue(sequence[i], out specificLever))
            {
                if (specificLever.isRotated && correctSequence)
                {
                    //Send next decal to activate and deactivate the previous
                    active = i + 1;
                }
                else if (i == 0)
                {
                    active = 0;
                    correctSequence = false;
                }
                else
                {
                    correctSequence = false;
                }
            }
        }

        if (correctSequence)
        {
            SignSequenceManager.Solved();
            notFinished = false;
            AllLeversRotated();
        }

        if (notFinished)
        {
            for (int i = 0; i < sequence.Length - 2; i++)
            {
                if (leverDictionary.TryGetValue(sequence[i], out specificLever))
                {
                    if (i == active)
                        SignSequenceManager.activateORdeactivateProjector(sequenceProjectors[i], true);
                    else
                        SignSequenceManager.activateORdeactivateProjector(sequenceProjectors[i], false);
                }
            }

            int lastActive = SignSequenceManager.ReturnIndexActiveMinusOne();
            for (int i = 0; i < sequence.Length; i++)
            {
                if (leverDictionary.TryGetValue(sequence[i], out specificLever))
                {
                    if (i > lastActive && specificLever.isRotated)
                    {
                        RestartAllLevers();
                    }
                }
            }
        }
    }

    private void RestartAllLevers()
    {
        foreach (KeyValuePair<string, RotateLever> leverEntry in leverDictionary)
        {
            RotateLever leverScript = leverEntry.Value;

            if (leverScript.isRotated)
            {
                leverScript.StartRotation();
            }
        }
    }


    private void AllLeversRotated()
    {
       foreach (KeyValuePair<string, RotateLever> leverEntry in leverDictionary)
       {
            RotateLever leverScript = leverEntry.Value;

            if (!leverScript.isRotated)
            {
                leverScript.StartRotation();
            }

            // Start the coroutine for the delay before calling NoMore
            StartCoroutine(CallNoMoreWithDelay(leverScript, 1.0f));
        }
    }

    private IEnumerator CallNoMoreWithDelay(RotateLever leverScript, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Call the NoMore method
        leverScript.NoMore();
    }
}
