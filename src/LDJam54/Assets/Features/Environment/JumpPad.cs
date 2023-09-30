using System;
using DG.Tweening;
using UnityEngine;
using Util;

namespace Features.Environment
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField]
        private float jumpForce = 10.0f;
        private void OnCollisionEnter(Collision other)
        {
            if(!other.gameObject.HasComponent<Rigidbody>()) return;
            
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            PlayAnimation();
        }
        
        private void PlayAnimation()
        {
            //get render material
            var material = GetComponent<Renderer>().material;
            
            material.DOFloat(1, "_OutlineWidth", 0.1f)
                .OnComplete(() => material.DOFloat(0.4f, "_OutlineWidth", 0.1f));
        }
    }
}