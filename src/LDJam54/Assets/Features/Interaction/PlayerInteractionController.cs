using UnityEngine;

namespace Features.Interaction
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private float objectSpeed = 10;
        [SerializeField] private float rotationSpeed = 300;
        [SerializeField] private Camera camera;
        [SerializeField] private float pickupDistance = 1f;
    
        private Maybe<InteractableObject> targetted = Maybe<InteractableObject>.Missing();
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
                if (interactable == null)
                {
                    targetted.IfPresent(x => x.Unhighlight());
                    targetted = Maybe<InteractableObject>.Missing();
                }
                else if (interactable != targetted.Value)
                {
                    targetted.IfPresent(x => x.Unhighlight());
                    targetted = interactable;
                    interactable.Highlight();
                }
            }
            else if (targetted.IsPresent)
            {
                targetted.Value.Unhighlight();
                targetted = Maybe<InteractableObject>.Missing();
            }
        }
    
        private void CheckForPickupOrSetDown()
        {
            if (held.IsMissing && targetted.IsPresent && targetted.Value.CanBeHeld && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
            {
                held = targetted.Value;
                targetted.Value.Hold();
                heldParent = targetted.Value.transform.parent;
                targetted.Value.transform.parent = transform;
            }
            else if (held.IsPresent && targetted.IsPresent && targetted.Value.CanBeSetOn && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
            {
                if (targetted.Value.SnapToCenter)
                    held.Value.SetDown(new Vector3(targetted.Value.transform.position.x, targetted.Value.transform.position.y + targetted.Value.Collider.bounds.extents.y + held.Value.Collider.bounds.extents.y + 0.1f, targetted.Value.transform.position.z), held.Value.transform.rotation, objectSpeed, rotationSpeed);
                else
                    held.Value.SetDown(held.Value.transform.position, held.Value.transform.rotation, objectSpeed, rotationSpeed);
                held.Value.transform.parent = heldParent.Value;
                held = Maybe<InteractableObject>.Missing();
            }
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