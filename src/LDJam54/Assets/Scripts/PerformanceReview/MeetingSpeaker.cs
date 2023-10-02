using FMOD.Studio;
using FMODUnity;

public class MeetingSpeaker : OnMessage<PlayMeetingBossMonologue>
{
    private EventInstance instance;

    protected override void Execute(PlayMeetingBossMonologue msg)
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