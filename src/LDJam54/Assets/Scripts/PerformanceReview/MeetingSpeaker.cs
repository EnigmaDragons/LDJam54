using FMOD.Studio;
using FMODUnity;

public class MeetingSpeaker : OnMessage<PlayMeetingBossMonologue>
{
    public StudioEventEmitter emitter;

    private EventInstance instance;

    protected override void Execute(PlayMeetingBossMonologue msg)
    {
        instance = emitter.EventInstance;
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