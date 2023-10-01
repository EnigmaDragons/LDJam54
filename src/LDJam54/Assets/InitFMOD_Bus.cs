
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class InitFMOD_Bus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RuntimeManager.GetBus("bus:/Music").setVolume(PlayerPrefs.GetFloat("bus:/Music", 0.5f));
        RuntimeManager.GetBus("bus:/SFX").setVolume(PlayerPrefs.GetFloat("bus:/SFX", 0.5f));
        RuntimeManager.GetBus("bus:/Music").setVolume(PlayerPrefs.GetFloat("bus:/UI", 0.5f));
    }
}
