using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Environment
{
    public class ObjectOnConveyor : MonoBehaviour
    {
        [SerializeField] private float speed = 1.5f;
        
        public Vector3 direction;
        
        public List<Vector3> directions = new List<Vector3>();
        
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
            //sum all directions and take the normal
            var sum = directions.Aggregate(Vector3.zero, (current, dir) => current + dir);
            direction = sum.normalized;
            
            rb.MovePosition(transform.position + direction * (speed * Time.fixedDeltaTime));
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
                directions.Clear();
            }
        }
    }
}