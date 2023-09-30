using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.PlayerControls
{
    [RequireComponent(typeof(FirstPersonController))]
    public class AdvPlayerController : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig;
        private FirstPersonController _fpController;
        private Rigidbody _rigidbody;
        
        [SerializeField]
        private float groundCheckDistance = 0.2f;
        
        private void Awake()
        {
            _fpController = GetComponent<FirstPersonController>();
            _rigidbody = GetComponent<Rigidbody>();
            
            _fpController.Speed = gameConfig.PlayerWalkSpeed;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) _fpController.Speed = gameConfig.PlayerRunSpeed;
            else if (Input.GetKeyUp(KeyCode.LeftShift)) _fpController.Speed = gameConfig.PlayerWalkSpeed;
            
            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) Jump();
        }
        

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        }
        
        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * gameConfig.PlayerJumpForce, ForceMode.Impulse);
        }

        private void OnDrawGizmosSelected()
        {
            //draw a line from the player to the ground
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        }
    }
}