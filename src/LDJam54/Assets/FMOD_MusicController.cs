using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMOD_MusicController : MonoBehaviour
{

    EventInstance menuMusic;
    int musicTrackNumber; 

    private void Start()
    {
        StartMenuMusic();
    }

    public void StartMenuMusic()
    {
        menuMusic = RuntimeManager.CreateInstance("event:/MUSIC/MenuMusic");
        menuMusic.start();
        menuMusic.release();
    }

    public void SelectMusic()
    {
        menuMusic.setParameterByName("", musicTrackNumber);
    }
}
