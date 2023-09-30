using System;
using UnityEngine;

namespace Util
{
    public class ColliderWrapper : MonoBehaviour
    {
        public Action<Collision> OnCollisionEnterAction;
        public Action<Collision> OnCollisionStayAction;
        public Action<Collision> OnCollisionExitAction;
        
        public Action<Collider> OnTriggerEnterAction;
        public Action<Collider> OnTriggerStayAction;
        public Action<Collider> OnTriggerExitAction;
        
        private void OnCollisionEnter(Collision other)
        {
            OnCollisionEnterAction?.Invoke(other);
        }
        
        private void OnCollisionStay(Collision other)
        {
            OnCollisionStayAction?.Invoke(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            OnCollisionExitAction?.Invoke(other);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterAction?.Invoke(other);
        }
        
        private void OnTriggerStay(Collider other)
        {
            OnTriggerStayAction?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitAction?.Invoke(other);
        }
        
    }
}