using FMOD.Studio;
using FMODUnity;

public class MeetingSpeaker : OnMessage<PlayBossMonologue>
{
    private EventInstance instance;

    protected override void Execute(PlayBossMonologue msg)
    {
        if (instance.isValid())
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
        instance = RuntimeManager.CreateInstance(msg.Speech);
        instance.start();
        instance.release();
    }
}
