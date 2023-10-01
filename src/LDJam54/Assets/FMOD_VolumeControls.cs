using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class FMOD_VolumeControls : MonoBehaviour
{
    [SerializeField] MixerVolumeSlider mixerVolSlider;
    Bus musicVol;


    private void Update()
    {
        GetFMODBusMusic();
    }
    public void GetFMODBusMusic()
    {
        musicVol = RuntimeManager.GetBus("bus:/Music");
        musicVol.setVolume(mixerVolSlider.fmodVol);
    }

}
