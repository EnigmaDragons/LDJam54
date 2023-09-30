using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

namespace Features.PlayerControls
{
    [RequireComponent(typeof(FirstPersonController))]
    public class AdvPlayerController : MonoBehaviour
    {
        private FirstPersonController _fpController;
        private Rigidbody _rigidbody;
        [SerializeField] AnimatorToSoundController animatorToSoundController;
        private bool isJumping;
        [SerializeField] float waitTime;
        [SerializeField] FMOD_FootStepManager footStepManager;

        [SerializeField]
        private float defaultSpeed = 5.0f;
        [SerializeField]
        private float sprintSpeed = 10.0f;
        
        [SerializeField]
        private float jumpHeight = 2.0f;
        
        [SerializeField]
        private float groundCheckDistance = 0.2f;
        
        private void Awake()    
        {
            _fpController = GetComponent<FirstPersonController>();
            _rigidbody = GetComponent<Rigidbody>();
            
            _fpController.Speed = defaultSpeed;
            isJumping = false;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _fpController.Speed = sprintSpeed;
                animatorToSoundController.StartRunning();
                footStepManager.SetFootstepRunningLength();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _fpController.Speed = defaultSpeed;
                animatorToSoundController.StopRunning();
                footStepManager.SetFootstepWalkLength();
            }

            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();    
            } 
            
            if(_fpController.m_velocity != Vector3.zero && IsGrounded() && !isJumping) 
            {
                animatorToSoundController.WalkAnimationStart();
            }
            else
            {
                animatorToSoundController.WalkAnimationStop();
            }
        }
        
        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        }
        
        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            animatorToSoundController.WalkAnimationStop();
            footStepManager.JumpSound(); 
            isJumping = true;
            StartCoroutine(JumpIsOver());
        }

        private void OnDrawGizmosSelected()
        {
            //draw a line from the player to the ground
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        }

        private IEnumerator JumpIsOver()
        {
           while (isJumping)
            {
                yield return new WaitForSeconds(waitTime);
                isJumping = false;
            }
        }
    }
}