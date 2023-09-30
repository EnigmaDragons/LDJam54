using UnityEngine;

namespace Features.Interaction
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private float objectSpeed = 10;
        [SerializeField] private float rotationSpeed = 300;
        [SerializeField] private Camera camera;
        [SerializeField] private float pickupDistance = 1f;
        [SerializeField] private LayerMask pickupLayerMask;
    
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
            if (held.IsMissing && targetted.IsPresent && targetted.Value.CanBeHeld && IsActionTriggered())
            {
                HoldObject();
            }
            else if (held.IsPresent && targetted.IsPresent && targetted.Value.CanBeSetOn && IsActionTriggered())
            {
                SetObject();
            }
            else if (held.IsPresent && IsActionTriggered())
            {
                DropObject();
            }
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
                position = new Vector3(targetted.Value.transform.position.x, targetted.Value.transform.position.y + targetted.Value.Collider.bounds.extents.y + held.Value.Collider.bounds.extents.y + 0.1f, targetted.Value.transform.position.z);
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
        
        private bool IsActionTriggered()
        {
            return Input.GetKeyDown("joystick button 0") 
                   || Input.GetKeyDown(KeyCode.E) 
                   || Input.GetMouseButtonDown(0);
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