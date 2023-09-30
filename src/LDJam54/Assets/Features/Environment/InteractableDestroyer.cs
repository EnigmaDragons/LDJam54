using System;
using UnityEngine;
using Util;

namespace Features.Environment
{
    public class InteractableDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.HasComponent<InteractableObject>())
                Destroy(other.gameObject);
        }
    }
}