using Features.Environment;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class SoundGuy : MonoBehaviour
{
    [SerializeField] private EventReference boxLifted; 
    [SerializeField] private EventReference boxSetDown;
    [SerializeField] private EventReference doorOpened;
    [SerializeField] private EventReference doorClosed;
    [SerializeField] private EventReference workingMusic;
    [SerializeField] private EventReference jumpPad;
    [SerializeField] private EventReference happyBossComment;
    [SerializeField] private EventReference unhappyBossComment;
    [SerializeField] private EventReference neutralBossComment;

    private EventInstance _currentMusic;
    
    private void OnEnable()
    {
        Message.Subscribe<ObjectPickedUp>(OnObjectPickedUp, this);
        Message.Subscribe<ObjectSetDown>(OnObjectSetDown, this);
        Message.Subscribe<WorkdayStarted>(OnWorkdayStarted, this);
        Message.Subscribe<WorkdayNearlyOver>(OnWorkdayNearlyOver, this);
        Message.Subscribe<WasFired>(OnWasFired, this);
        Message.Subscribe<DoorOpened>(OnDoorOpened, this);
        Message.Subscribe<DoorClosed>(OnDoorClosed, this);
        Message.Subscribe<TeleporterActivated>(OnTeleporterActivated, this);
        Message.Subscribe<JumpPadUsed>(OnJumpPadUsed, this);
        Message.Subscribe<PlayBossComment>(OnBossComment, this);
    }

    private void OnBossComment(PlayBossComment obj)
    {
        if (obj.Sentiment == BossSentiment.Happy)
            PlayOneShot(happyBossComment, Vector3.zero);
        if (obj.Sentiment == BossSentiment.Unhappy)
            PlayOneShot(unhappyBossComment, Vector3.zero);
        if (obj.Sentiment == BossSentiment.Neutral)
            PlayOneShot(neutralBossComment, Vector3.zero);
    }

    private void OnWasFired(WasFired obj)
    {
        
    }

    private void OnJumpPadUsed(JumpPadUsed obj)
    {
        PlayOneShot(jumpPad, obj.Position);
    }

    private void OnTeleporterActivated(TeleporterActivated obj)
    {
        
    }

    private void OnDoorClosed(DoorClosed obj)
    {
        PlayOneShot(doorClosed, obj.Position);
    }

    private void OnDoorOpened(DoorOpened obj)
    {
        PlayOneShot(doorOpened, obj.Position);
    }

    private void OnWorkdayNearlyOver(WorkdayNearlyOver obj)
    {
        _currentMusic.setParameterByName("WorkdayNearlyOver", 1);
    }

    private void OnWorkdayStarted(WorkdayStarted obj)
    {
        _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
        _currentMusic = RuntimeManager.CreateInstance(workingMusic);
        _currentMusic.start();
    }

    private void OnDisable() => Message.Unsubscribe(this);

    private void OnObjectSetDown(ObjectSetDown obj)
    {
        if (obj.ObjectType == ObjectType.Box)
            PlayOneShot(boxSetDown, obj.Position);
    }

    private void OnObjectPickedUp(ObjectPickedUp obj)
    {
        if (obj.ObjectType == ObjectType.Box)
            PlayOneShot(boxLifted, obj.Position);
    }

    private void PlayOneShot(EventReference eventName, Vector3 pos)
        => RuntimeManager.PlayOneShot(eventName, pos);
}
