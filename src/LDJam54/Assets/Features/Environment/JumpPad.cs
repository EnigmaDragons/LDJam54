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
            if(!other.gameObject.TryGetComponent(out Rigidbody rb)) return;

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Message.Publish(new JumpPadUsed(transform.position));
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