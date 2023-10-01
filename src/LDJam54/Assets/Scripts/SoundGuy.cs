using FMODUnity;
using UnityEngine;

public class SoundGuy : MonoBehaviour
{
    [SerializeField] private EventReference boxPickUp;
    [SerializeField] private EventReference boxSetDown;
    
    private void OnEnable()
    {
        Message.Subscribe<ObjectPickedUp>(OnObjectPickedUp, this);
        Message.Subscribe<ObjectSetDown>(OnObjectSetDown, this);
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
