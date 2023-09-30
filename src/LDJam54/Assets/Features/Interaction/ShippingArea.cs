using System;
using Messages;
using UnityEngine;
using Util;

namespace Features.Interaction
{
    public class ShippingArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out InteractableObject interactable))
            {
                interactable.PlayShippedAnimation();
                DeliverBox();
            }
        }

        private void DeliverBox()
        {
            var message = new BoxDelivered();
            Message.Publish(message);
            CurrentGameState.IncrementKPIStatic(KPI.BoxShipped, 1);
        }
    }
}