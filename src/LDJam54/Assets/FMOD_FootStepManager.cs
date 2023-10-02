using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMOD_FootStepManager : MonoBehaviour
{

    EventInstance footstep;

    private void Start()
    {
        footstep = RuntimeManager.CreateInstance("event:/CHAR/FootSteps");
    }

    public void FootStepDown()
    {
        footstep.start();
    }

    private void OnDestroy()
    {
        footstep.release();
    }

    public void JumpSound()
    {
        RuntimeManager.PlayOneShot("event:/CHAR/JumpLand");
    } 

    public void SetFootstepRunningLength()
    {
        footstep.setParameterByName("FootStepLength", 1.0f);
    }

    public void SetFootstepWalkLength() 
    {
        footstep.setParameterByName("FootStepLength", 0f);
    }

    public void SetTerrainMetal()
    {
        footstep.setParameterByName("Terrain", 2);
    }   
}
