using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMOD_FootStepManager : MonoBehaviour
{

    EventInstance footstep; 
    
    public void FootStepDown()
    {
        footstep = RuntimeManager.CreateInstance("event:/CHAR/FootSteps");
        footstep.start();
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
    
}
