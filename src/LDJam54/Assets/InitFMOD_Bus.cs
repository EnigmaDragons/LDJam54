
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
        RuntimeManager.GetBus("bus:/Music").setVolume(PlayerPrefs.GetFloat ("bus:/Music"));
        RuntimeManager.GetBus("bus:/SFX").setVolume(PlayerPrefs.GetFloat("bus:/SFX"));
        RuntimeManager.GetBus("bus:/Music").setVolume(PlayerPrefs.GetFloat("bus:/UI"));
    }
}
