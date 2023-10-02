using FMODUnity;

public class MeetingSpeaker : OnMessage<PlayMeetingBossMonologue>
{
    public StudioEventEmitter emitter;
    
    protected override void Execute(PlayMeetingBossMonologue msg)
    {
        emitter.Stop();
        emitter.EventReference = msg.Speech;
        emitter.Play();
    }
}
