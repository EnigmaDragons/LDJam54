using UnityEngine;
using Util;

namespace Features.Environment
{
    public class BoxTeleporter : MonoBehaviour
    {
        [SerializeField]
        private Transform teleportTarget;
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.HasComponent<InteractableObject>()) return;
            other.transform.position = teleportTarget.position;
        }
    }
}