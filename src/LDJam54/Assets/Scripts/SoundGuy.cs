using Features.Environment;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class SoundGuy : MonoBehaviour
{
    [SerializeField] private EventReference boxLifted; 
    [SerializeField] private EventReference boxSetDown;
    [SerializeField] private EventReference incineratorUsed;
    [SerializeField] private EventReference doorOpened;
    [SerializeField] private EventReference doorClosed;
    [SerializeField] private EventReference workingMusic;
    [SerializeField] private EventReference workingMusicDay2;
    [SerializeField] private EventReference franticMusic;
    [SerializeField] private EventReference gameOverMusic;
    [SerializeField] private EventReference workAssesmentMusic;
    [SerializeField] private EventReference sleepMusic;
    [SerializeField] private EventReference jumpPad;
    [SerializeField] private EventReference shippingTeleporter;
    [SerializeField] private EventReference happyBossComment;
    [SerializeField] private EventReference unhappyBossComment;
    [SerializeField] private EventReference neutralBossComment;

    private EventInstance _currentMusic;
    
    private void OnEnable()
    {
        Message.Subscribe<ObjectPickedUp>(OnObjectPickedUp, this);
        Message.Subscribe<ObjectSetDown>(OnObjectSetDown, this);
        Message.Subscribe<IncineratorUsed>(OnIncineratorUsed, this);
        Message.Subscribe<WorkdayStarted>(OnWorkdayStarted, this);
        Message.Subscribe<WorkdayNearlyOver>(OnWorkdayNearlyOver, this);
        Message.Subscribe<WorkdayEnded>(OnWorkdayEnded, this);
        Message.Subscribe<WasFired>(OnWasFired, this);
        Message.Subscribe<DoorOpened>(OnDoorOpened, this);
        Message.Subscribe<DoorClosed>(OnDoorClosed, this);
        Message.Subscribe<TeleporterActivated>(OnTeleporterActivated, this);
        Message.Subscribe<JumpPadUsed>(OnJumpPadUsed, this);
        Message.Subscribe<PlayBossComment>(OnBossComment, this);
        Message.Subscribe<StartKpiMeetingRequested>(OnMorningMeetingStarted, this);
        Message.Subscribe<SleepTimeout>(OnSleepTimeOut , this);

    }

    private void OnMorningMeetingStarted(StartKpiMeetingRequested obj)
    {
        
    }

    private void OnIncineratorUsed(IncineratorUsed obj)
    {
        PlayOneShot(incineratorUsed, obj.Position);
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
        _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
        _currentMusic = RuntimeManager.CreateInstance(gameOverMusic);
        _currentMusic.start(); 
    }

    private void OnJumpPadUsed(JumpPadUsed obj)
    {
        PlayOneShot(jumpPad, obj.Position);
    }

    private void OnTeleporterActivated(TeleporterActivated obj)
    {
        PlayOneShot(shippingTeleporter, obj.Position);
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
        var currentDay = CurrentGameState.State.CurrentDayNumber;
        if(currentDay == 1)
        {
            _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
            _currentMusic = RuntimeManager.CreateInstance(workingMusic);
            _currentMusic.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            _currentMusic.start();
        }

        if (currentDay == 2)
        {
            _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
            _currentMusic = RuntimeManager.CreateInstance(workingMusicDay2);
            _currentMusic.start();
        }


        if (currentDay == 3)
        {
            _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
            _currentMusic = RuntimeManager.CreateInstance(workingMusicDay2);
            _currentMusic.start();
        }

        if (currentDay == 4)
        {
            _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
            _currentMusic = RuntimeManager.CreateInstance(franticMusic);
            _currentMusic.start();
        }

        if (currentDay == 5)
        {
            _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
            _currentMusic = RuntimeManager.CreateInstance(franticMusic);
            _currentMusic.start();
        }
    }

    private void OnWorkdayEnded(WorkdayEnded obj)
    {
        _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
        _currentMusic = RuntimeManager.CreateInstance(workAssesmentMusic);
        _currentMusic.start();
    }

    private void OnSleepTimeOut(SleepTimeout obj)
    {
        _currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
        _currentMusic = RuntimeManager.CreateInstance(sleepMusic);
        _currentMusic.start();
        Debug.Log("Sleep?");
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
