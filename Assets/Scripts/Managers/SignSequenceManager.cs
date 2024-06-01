using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SignSequenceManager : MonoBehaviour
{
    private DecalProjectorInfo[] decalProjectorsInfo;

    // Enum to represent the state of each DecalProjector
    public enum DecalState
    {
        Inactive,
        Active
    }

    private bool notFinished = true;

    private struct DecalProjectorInfo
    {
        public DecalProjector projector;
        public DecalState state;

        public DecalProjectorInfo(DecalProjector projector, DecalState state)
        {
            this.projector = projector;
            this.state = state;
        }
    }

    void Start()
    {
        DecalProjector[] decalProjectors = GetComponentsInChildren<DecalProjector>();
        decalProjectorsInfo = new DecalProjectorInfo[decalProjectors.Length];

        for (int i = 0; i < decalProjectors.Length; i++)
        {
            decalProjectorsInfo[i].projector = decalProjectors[i];
            decalProjectorsInfo[i].state = (i == 0) ? DecalState.Active : DecalState.Inactive;
        }

        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        if(notFinished)
            UpdateState();
    }

    void UpdateState()
    {
        for (int i = 0; i < decalProjectorsInfo.Length; i++)
        {
            if (decalProjectorsInfo[i].state == DecalState.Inactive && decalProjectorsInfo[i].projector.enabled)
            {
                decalProjectorsInfo[i].projector.enabled = false;
            }
            else if (decalProjectorsInfo[i].state == DecalState.Active && !decalProjectorsInfo[i].projector.enabled)
            {
                decalProjectorsInfo[i].projector.enabled = true;
            }
        }
    }


    public void activateORdeactivateProjector(string ProjectorName, bool active)
    {
        for (int i = 0; i < decalProjectorsInfo.Length; i++) {

            if (decalProjectorsInfo[i].projector.gameObject.name == ProjectorName)
            {
                if (active && !decalProjectorsInfo[i].projector.enabled)
                {
                    decalProjectorsInfo[i].state = DecalState.Active;
                }
                else if (!active && decalProjectorsInfo[i].projector.enabled)
                {
                    decalProjectorsInfo[i].state = DecalState.Inactive;
                }
            }
        }
    }

    public int ReturnIndexActiveMinusOne()
    {
        for (int i = 0; i < decalProjectorsInfo.Length; i++)
        {
            if (decalProjectorsInfo[i].projector.enabled)
            {
                return i;
            }
        }
        return -1;
    }

    public void Solved()
    {
        notFinished = false;
        foreach (DecalProjectorInfo decalProjectorsInfo in decalProjectorsInfo)
        {
            decalProjectorsInfo.projector.enabled = true;
        }
    }
}
