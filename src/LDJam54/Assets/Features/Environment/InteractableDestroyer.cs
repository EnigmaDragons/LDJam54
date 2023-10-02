using UnityEngine;
using Util;

namespace Features.Environment
{
    public class InteractableDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.HasComponent<InteractableObject>())
            {
                Message.Publish(new IncineratorUsed(other.transform.position));
                Destroy(other.gameObject);
            }
        }
    }
}