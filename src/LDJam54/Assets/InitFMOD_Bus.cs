
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
        RuntimeManager.GetBus("bus:/MSTBUS/Music").setVolume(PlayerPrefs.GetFloat("bus:/MSTBUS/Music", 0.5f));
        RuntimeManager.GetBus("bus:/MSTBUS/SFX").setVolume(PlayerPrefs.GetFloat("bus:/MSTBUS/SFX", 0.5f));
        RuntimeManager.GetBus("bus:/MSTBUS/UI").setVolume(PlayerPrefs.GetFloat("bus:/MSTBUS/UI", 0.5f));
        RuntimeManager.GetBus("bus:/MSTBUS").setVolume(PlayerPrefs.GetFloat("bus:/MSTBUS", 0.5f));
        RuntimeManager.GetBus("bus:/MSTBUS/DX").setVolume(PlayerPrefs.GetFloat("bus:/MSTBUS/DX", 0.5f));
    }
}
