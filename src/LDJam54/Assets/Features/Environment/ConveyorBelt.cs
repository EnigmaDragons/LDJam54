using System;
using UnityEngine;
using Util;

namespace Features.Environment
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField] 
        private ColliderWrapper topCollider;
        private void Awake()
        {
            topCollider.OnTriggerEnterAction += OnTriggerEnter;
            topCollider.OnTriggerExitAction += OnTriggerExit;
        }

        private void OnTriggerEnter(Collider obj)
        {
            if(obj.TryGetComponent(out ObjectOnConveyor objectOnConveyor))
                objectOnConveyor.directions.Add(-transform.forward);
        }

        private void OnTriggerExit(Collider obj)
        {
            if(obj.TryGetComponent(out ObjectOnConveyor objectOnConveyor))
                objectOnConveyor.directions.Remove(-transform.forward);
        }
    }
}