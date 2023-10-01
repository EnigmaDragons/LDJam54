using System;
using System.Collections;
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

        private void Start()
        {
            StartCoroutine(DoConveyorCheckCoroutine());
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        
        private IEnumerator DoConveyorCheckCoroutine()
        {
            while (true)
            {
                DoConveyorCheck();
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        private void DoConveyorCheck()
        {
            var mask = LayerMask.GetMask("ConveyorBelt");
            if (!Physics.Raycast(transform.position, Vector3.down, out var hit, 2.0f, layerMask: mask))
            {
                Destroy(this);
            }
        }
    }
}