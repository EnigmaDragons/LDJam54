using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FMOD_MenuMusic : MonoBehaviour
{
    EventInstance menuMusic;

    private void Start()
    {
        menuMusic = RuntimeManager.CreateInstance("event:/MUSIC/MenuMusic");
        menuMusic.start();
        menuMusic.release();
    }


    public void StopMenuMusic()
    {
        menuMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopMenuMusicFade()
    {
        menuMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
