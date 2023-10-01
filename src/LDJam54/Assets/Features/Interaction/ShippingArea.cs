using Messages;
using UnityEngine;

namespace Features.Interaction
{
    public class ShippingArea : MonoBehaviour
    {
        [SerializeField] private SortingColor color;
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.TryGetComponent(out InteractableObject interactable)) return;
            if(!other.gameObject.TryGetComponent(out ColoredBox box)) return;
            if (color != SortingColor.All && color != SortingColor.None && box.Color != color) return;
            
            interactable.PlayShippedAnimation();
            DeliverBox();
        }

        private void DeliverBox()
        {
            var message = new BoxDelivered(color);
            Message.Publish(message);
            Message.Publish(new TeleporterActivated(transform.position));
            CurrentGameState.IncrementKPIStatic(KPI.BoxesShipped, 1);
        }
    }
}