using UnityEngine;
    
public class Incinerator : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var component = other.collider.GetComponent<InteractableObject>();
        if (component == null)
            return;
        Message.Publish(new IncineratorUsed(other.transform.position));
        Message.Publish(new ObjectTeleported(component));
        component.PlayBurningAnimation();
    }
}