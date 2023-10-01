using System;
using Messages;
using UnityEngine;
using Util;

namespace Features.Interaction
{
    public class ShippingArea : MonoBehaviour
    {
        [SerializeField] private SortingColor color;
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.TryGetComponent(out InteractableObject interactable)) return;
            if(!other.gameObject.TryGetComponent(out ColoredBox box)) return;
            if (box.Color != color) return;
            
            interactable.PlayShippedAnimation();
            DeliverBox();
        }

        private void DeliverBox()
        {
            var message = new BoxDelivered(color);
            Message.Publish(message);
            CurrentGameState.IncrementKPIStatic(KPI.BoxesShipped, 1);
        }
    }
}