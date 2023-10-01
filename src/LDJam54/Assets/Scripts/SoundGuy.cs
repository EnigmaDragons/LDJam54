using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SoundGuy : MonoBehaviour
{
    [SerializeField] private EventReference boxPickUp;
    [SerializeField] private EventReference boxSetDown;
    [SerializeField] private EventReference doorOpened;
    [SerializeField] private EventReference workingMusic;

    private EventInstance _currentMusic;
    
    private void OnEnable()
    {
        Message.Subscribe<ObjectPickedUp>(OnObjectPickedUp, this);
        Message.Subscribe<ObjectSetDown>(OnObjectSetDown, this);
        Message.Subscribe<WorkdayStarted>(OnWorkdayStarted, this);
        Message.Subscribe<WorkdayNearlyOver>(OnWorkdayNearlyOver, this);
        Message.Subscribe<DoorOpened>(OnDoorOpened, this);
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
            PlayOneShot(boxPickUp, obj.Position);
    }

    private void PlayOneShot(EventReference eventName, Vector3 pos)
        => RuntimeManager.PlayOneShot(eventName, pos);
}
