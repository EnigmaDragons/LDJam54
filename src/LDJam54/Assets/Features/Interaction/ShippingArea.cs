using UnityEngine;

namespace Features.Interaction
{
    public class ShippingArea : MonoBehaviour
    {
        private Collider _lastCollider;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_lastCollider == other) return;
            _lastCollider = other;
            if(!other.gameObject.TryGetComponent(out InteractableObject interactable)) return;
            if(!other.gameObject.TryGetComponent(out ColoredBox box)) return;
            
            Message.Publish(new ObjectTeleported(interactable));
            interactable.PlayShippedAnimation();
            Message.Publish(new BoxShipped(box));
            Message.Publish(new TeleporterActivated(transform.position));
        }
    }
}