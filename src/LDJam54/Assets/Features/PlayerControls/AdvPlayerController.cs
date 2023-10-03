using System.Linq;
using UnityEngine;
using System.Collections;

namespace Features.PlayerControls
{
    [RequireComponent(typeof(FirstPersonController))]
    public class AdvPlayerController : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig;
        private FirstPersonController _fpController;
        private Rigidbody _rigidbody;
        [SerializeField] AnimatorToSoundController animatorToSoundController;
        private bool isJumping;
        [SerializeField] float waitTime;
        [SerializeField] FMOD_FootStepManager footStepManager;

        [SerializeField]
        private float groundCheckDistance = 0.2f;

        public bool IsCurrentlyGrounded = true;
        
        private void Awake()    
        {
            _fpController = GetComponent<FirstPersonController>();
            _rigidbody = GetComponent<Rigidbody>();
            
            _fpController.Speed = gameConfig.PlayerWalkSpeed;
            isJumping = false;
        }
        
        private void Update()
        {
            if (!FirstPersonInteractionStatus.IsEnabled)
            {
                animatorToSoundController.WalkAnimationStop();
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || ControllerChecker.IsLeftStickDown())
            {
                _fpController.Speed = gameConfig.PlayerRunSpeed;
                animatorToSoundController.StartRunning();
                footStepManager.SetFootstepRunningLength();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || ControllerChecker.IsLeftStickUp())
            {
                _fpController.Speed = gameConfig.PlayerWalkSpeed;
                animatorToSoundController.StopRunning();
                footStepManager.SetFootstepWalkLength();
            }

            if ((Input.GetKeyDown(KeyCode.Space) || ControllerChecker.IsA()) && IsGrounded())
            {
                Jump();    
            }
            else if ((Input.GetKeyDown(KeyCode.U) || ControllerChecker.IsY()))
            {
                var position = _rigidbody.transform.position;
                position.y++;
                _rigidbody.transform.position = position;
            }

            if (_fpController.m_velocity != Vector3.zero && IsGrounded() && !isJumping) 
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
            IsCurrentlyGrounded = Physics.RaycastAll(transform.position, Vector3.down, groundCheckDistance).Any(o => o.collider.gameObject != gameObject);
            return IsCurrentlyGrounded;
        }
        
        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * gameConfig.PlayerJumpForce, ForceMode.Impulse);
            animatorToSoundController.WalkAnimationStop();
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
               footStepManager.JumpSound(); 
           }
        }
    }
}