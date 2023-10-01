using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UIElements;

public class FMOD_VolumeControls : MonoBehaviour
{
    [SerializeField] MixerVolumeSlider musicVolSlider;
    [SerializeField] MixerVolumeSlider sfxVolSlider;
    [SerializeField] MixerVolumeSlider uiVolSlider;

    Bus musicVol;
    Bus sfxVol;
    Bus uiVol;


    private void Update()
    {
        GetFMODBusMusic();
        GetFMODBusSFX();
        GetFMODBusUI();
    }
    public void GetFMODBusMusic()
    {
        musicVol = RuntimeManager.GetBus("bus:/Music");
        musicVol.setVolume(musicVolSlider.fmodVol);
    }

    public void GetFMODBusSFX()
    {
        sfxVol = RuntimeManager.GetBus("bus:/SFX");
        sfxVol.setVolume(sfxVolSlider.fmodVol);
    }

    public void GetFMODBusUI()
    {
        uiVol = RuntimeManager.GetBus("bus:/UI");
        uiVol.setVolume(uiVolSlider.fmodVol);
    }


    

}
