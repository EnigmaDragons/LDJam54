using System;
using UnityEngine;
using Util;

namespace Features.Environment
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField] 
        private ColliderWrapper topCollider;
        [SerializeField]
        private float speed = 1.0f;
        private void Awake()
        {
            topCollider.OnTriggerEnterAction += OnTriggerEnter;
            topCollider.OnTriggerExitAction += OnTriggerExit;
        }

        private void OnTriggerEnter(Collider obj)
        {
            if(!obj.gameObject.HasComponent<Rigidbody>()) return;
            obj.gameObject.AddComponent<ObjectOnConveyor>().velocity = transform.forward * -speed;
        }

        private void OnTriggerExit(Collider obj)
        {
            if(!obj.gameObject.HasComponent<Rigidbody>()) return;
            Destroy(obj.gameObject.GetComponent<ObjectOnConveyor>());
        }
    }
}