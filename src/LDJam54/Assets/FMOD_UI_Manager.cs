using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity; 

public class FMOD_UI_Manager : MonoBehaviour
{
    EventInstance hooverSound;
    EventInstance creditMusic;
    public void ClickButton()
    {
        RuntimeManager.PlayOneShot("event:/UI/UIButtons");
        hooverSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); 
    }

    public void PlayeHooverSound()
    {
        hooverSound = RuntimeManager.CreateInstance("event:/UI/UIHoover");
        hooverSound.start();
        hooverSound.release();
    }

    public void StopHooverSound()
    {
        hooverSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); 
    }

    public void PlayCreditMusic()
    {
        creditMusic = RuntimeManager.CreateInstance("event:/MUSIC/CreditMusic");
        creditMusic.start();
        creditMusic.release();
    }
}
