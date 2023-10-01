using System;
using UnityEngine;

namespace Features.Environment
{
    public class ObjectOnConveyor : MonoBehaviour
    {
        public Vector3 velocity;

        private Rigidbody rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}