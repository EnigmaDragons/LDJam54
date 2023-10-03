using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class FmodSceneMusic : OnMessage<StopCurrentBackgroundMusic>
{
    public EventReference music;
    
    private EventInstance _musicInstance;
    
    protected override void AfterEnable()
    {
        _musicInstance = RuntimeManager.CreateInstance(music);
        _musicInstance.start();
        _musicInstance.release();
    }

    protected override void AfterDisable()
    {
        _musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

    protected override void Execute(StopCurrentBackgroundMusic msg)
    {
        _musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
