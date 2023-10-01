
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Forklift_FMODController : MonoBehaviour
{
    EventInstance forkLiftSounds;


    public void StartForkliftSound() //it goes automatically to idle after this. 
    {
        forkLiftSounds = RuntimeManager.CreateInstance("event:/ForkLift");
        forkLiftSounds.set3DAttributes(RuntimeUtils.To3DAttributes (gameObject));
        forkLiftSounds.start();
        forkLiftSounds.release();
        forkLiftSounds.setParameterByName("ForkLiftDriveStages", 0);
    }

    public void StarForkLiftDriving()//start moving 
    {
        forkLiftSounds.setParameterByName("ForkLiftDriveStages", 1);
    }

    public void ForkliftToIdle()//back to stationary
    {
        forkLiftSounds.setParameterByName("ForkLiftDriveStages", 0);
    }

    public void ToForkliftingActivity()//start lifting something
    {
        forkLiftSounds.setParameterByName("ForkLiftDriveStages", 2);
    }

    public void StopForkliftingActivity()//stop lifting something
    {
        forkLiftSounds.setParameterByName("ForkLiftDriveStages", 3);
    }

    public void StopForklit()//stop alltogether
    {
        forkLiftSounds.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); //it goes to a stop sound automatically. Can be called at any point.
    }





}
