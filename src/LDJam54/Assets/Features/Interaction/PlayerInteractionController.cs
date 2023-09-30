using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Interaction
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private float objectSpeed = 10;
        [SerializeField] private float rotationSpeed = 300;
        [SerializeField] private Camera camera;
        [SerializeField] private float pickupDistance = 1f;
        [SerializeField] private float maxThrowForce;
        [SerializeField] private float minThrowForce;
        [SerializeField] private float throwForceIncreaseSpeed;
        [SerializeField] private Slider throwForceSlider;
        
        /// <summary>
        /// The interactable object that the player is currently looking at
        /// </summary>
        private Maybe<InteractableObject> targetted = Maybe<InteractableObject>.Missing();
        /// <summary>
        /// The interactable object that the player is currently holding
        /// </summary>
        private Maybe<InteractableObject> held = Maybe<InteractableObject>.Missing();
        private Maybe<Transform> heldParent = Maybe<Transform>.Missing();
        
        private void Update()
        {
            UpdateTarget();
            CheckForPickupOrSetDown();
        }

        private void FixedUpdate()
        {
            UpdateHeldObject();
        }

        private void UpdateTarget()
        {
            var ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, maxDistance: pickupDistance , hitInfo: out var hit))
            {
                var interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
                HandleInteractable(interactable);
            }
            else if (targetted.IsPresent)
            {
                targetted.Value.Unhighlight();
                targetted = Maybe<InteractableObject>.Missing();
            }
        }
    
        private void HandleInteractable(InteractableObject interactable)
        {
            if (interactable == null)
            {
                UnhighlightAndSetMissing();
            }
            else if (interactable != targetted.Value)
            {
                UnhighlightAndSetMissing();
                interactable.Highlight();
                targetted = interactable;
            }
        }
        
        private void UnhighlightAndSetMissing()
        {
            targetted.IfPresent(x => x.Unhighlight());
            targetted = Maybe<InteractableObject>.Missing();
        }
        private void CheckForPickupOrSetDown()
        {
            if (held.IsPresent && IsThrowActionTriggered())
            {
                ThrowObject();
                return;   
            }
            
            if (held.IsMissing && targetted.IsPresent && targetted.Value.CanBeHeld && IsDropActionTriggered())
            {
                HoldObject();
                return;
            }
            
            
            if (held.IsPresent && targetted.IsPresent && targetted.Value.CanBeSetOn && IsDropActionTriggered())
            {
                SetObject();
                return;
            }
            
            if (held.IsPresent && IsDropActionTriggered())
            {
                DropObject();
                return;
            }
            
        }

        private void ThrowObject()
        {
            StartCoroutine(ThrowObjectCoroutine());
        }

        private IEnumerator ThrowObjectCoroutine()
        {
            //while button is held
            var throwForce = minThrowForce;
            
            throwForceSlider.gameObject.SetActive(true);
            throwForceSlider.maxValue = maxThrowForce;
            throwForceSlider.minValue = minThrowForce;
            throwForceSlider.value = throwForce;
            var throwRange = maxThrowForce - minThrowForce;
            
            while (IsThrowActionHeld())
            {
                throwForce = (Mathf.Sin(Time.time * throwForceIncreaseSpeed) +1)/2 * throwRange + minThrowForce;
                throwForceSlider.value = throwForce;
                yield return null;
            }
            
            throwForceSlider.gameObject.SetActive(false);

            var lookDirection = camera.transform.forward;
            held.Value.Throw(lookDirection, throwForce);
            held.Value.transform.parent = heldParent.Value;
            held = Maybe<InteractableObject>.Missing();
        }
        
        private void DropObject()
        {
            held.Value.Release();
            held.Value.transform.parent = heldParent.Value;
            held = Maybe<InteractableObject>.Missing();
        }

        private void HoldObject()
        {
            held = targetted.Value;
            targetted.Value.Hold();
            heldParent = targetted.Value.transform.parent;
            targetted.Value.transform.parent = transform;
        }
        
        private void SetObject()
        {
            Vector3 position;
            if (targetted.Value.SnapToCenter) 
            {
                position = new Vector3(targetted.Value.transform.position.x, targetted.Value.transform.position.y + targetted.Value.Collider.bounds.extents.y + held.Value.Bounds.extents.y, targetted.Value.transform.position.z);
            }
            else 
            {
                position = held.Value.transform.position;
            }
            
            Quaternion rotation = held.Value.transform.rotation;
            held.Value.SetDown(position, rotation, objectSpeed, rotationSpeed);
            held.Value.transform.parent = heldParent.Value;
            held = Maybe<InteractableObject>.Missing();
        }
        
        private bool IsDropActionTriggered()
        {
            return Input.GetKeyDown("joystick button 0") 
                   || Input.GetKeyDown(KeyCode.E) 
                   || Input.GetMouseButtonDown(0);
        }

        private bool IsDropActionHeld()
        {
            return Input.GetKey("joystick button 0") 
                   || Input.GetKey(KeyCode.E) 
                   || Input.GetMouseButton(0);
        }
        
        private bool IsThrowActionTriggered()
        {
            return Input.GetKeyDown("joystick button 1") 
                   || Input.GetKeyDown(KeyCode.Q) 
                   || Input.GetMouseButtonDown(1);
        }
        
        private bool IsThrowActionHeld()
        {
            return Input.GetKey("joystick button 1") 
                   || Input.GetKey(KeyCode.Q) 
                   || Input.GetMouseButton(1);
        }

        private float bobbingTime = 0;

        
        private void UpdateHeldObject()
        {
            held.IfPresent(x =>
            {
                x.Body.MoveRotation(Quaternion.RotateTowards(x.transform.rotation, transform.rotation, rotationSpeed * Time.fixedDeltaTime));
                
                var isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
                if (isMoving) bobbingTime += Time.fixedDeltaTime;
                var yoffset = Mathf.Sin(bobbingTime*10) * x.BobbingIntensity;
                var targetPosition = transform.position + transform.forward * x.DistanceInFront + new Vector3(0, x.HeightOffset, 0) + new Vector3(0, yoffset, 0);
                x.Body.MovePosition(Vector3.MoveTowards(x.transform.position, targetPosition, objectSpeed * Time.fixedDeltaTime));
            });
        }
    }
}