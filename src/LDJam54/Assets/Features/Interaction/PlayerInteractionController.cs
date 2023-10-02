using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Interaction
{
    public class PlayerInteractionController : OnMessage<ObjectTeleported>
    {
        private static int interactableLayer = 1 << 3;
        private static int dropLayer = 1 << 6;
        private static int smallDropLayer = 1 << 7;
        private static int mediumDropLayer = 1 << 8;
        private static int largeDropLayer = 1 << 9;
        
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
        /// <summary>
        /// The interactable object that the player is currently looking at
        /// </summary>
        private Maybe<DropTarget> dropTarget = Maybe<DropTarget>.Missing();
        private bool somethingOnDropTarget;
        
        private void Update()
        {
            UpdateTarget();
            UpdateControlsShown();
            CheckForPickupOrSetDown();
        }

        private void FixedUpdate()
        {
            UpdateHeldObject();
        }

        private void UpdateTarget()
        {
            var ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, maxDistance: pickupDistance , hitInfo: out hit, layerMask: interactableLayer))
            {
                var interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
                HandleInteractable(interactable);
            }
            else if (targetted.IsPresent)
            {
                targetted.Value.Unhighlight();
                targetted = Maybe<InteractableObject>.Missing();
            }

            if (held.IsMissing)
                return;
            var sizeLayer = held.Value.DropSize == DropSize.Small
                ? smallDropLayer
                : held.Value.DropSize == DropSize.Medium
                    ? mediumDropLayer
                    : largeDropLayer;
            if (Physics.Raycast(ray, maxDistance: pickupDistance, hitInfo: out hit, layerMask: dropLayer | sizeLayer))
            {
                dropTarget = hit.collider.GetComponent<DropTarget>();
                somethingOnDropTarget = Physics.Raycast(dropTarget.Value.TopmostDropTarget.transform.position + Vector3.down * 0.1f, Vector3.up, out var _, held.Value.Bounds.size.y, interactableLayer);
            }
            else
                dropTarget = Maybe<DropTarget>.Missing();
        }

        public bool _isThrowing;
        public string _interactText;
        public bool _canThrow;
        public void UpdateControlsShown()
        {
            if (!_isThrowing && held.IsMissing && targetted.IsPresent && targetted.Value.CanBeHeld && _interactText != "Pickup")
            {
                _interactText = "Pickup";
                Message.Publish(new UpdateInteractControl(_interactText));   
            }
            else if (((held.IsMissing && targetted.IsMissing) || _isThrowing) && _interactText != "")
            {
                _interactText = "";
                Message.Publish(new UpdateInteractControl(_interactText)); 
            }
            else if (!_isThrowing && CanSetDown() && _interactText != "Place")
            {
                _interactText = "Place";
                Message.Publish(new UpdateInteractControl(_interactText)); 
            }
            else if (!_isThrowing && held.IsPresent && !CanSetDown() && _interactText != "Drop")
            {
                _interactText = "Drop";
                Message.Publish(new UpdateInteractControl(_interactText)); 
            }

            if (_canThrow && (held.IsMissing || _isThrowing))
            {
                _canThrow = false;
                Message.Publish(new SetCanThrow(_canThrow)); 
            }
            else if (!_canThrow && held.IsPresent && !_isThrowing)
            {
                _canThrow = true;
                Message.Publish(new SetCanThrow(_canThrow)); 
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
            
            if (CanSetDown() && IsDropActionTriggered())
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

        private bool CanSetDown()
        {
            return held.IsPresent && dropTarget.IsPresent && !somethingOnDropTarget;
        }

        private void ThrowObject()
        {
            StartCoroutine(ThrowObjectCoroutine());
        }

        private IEnumerator ThrowObjectCoroutine()
        {
            _isThrowing = true;
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
            _isThrowing = false;
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
            held.Value.SetDown(dropTarget.Value.TopmostDropTarget.GetLocation(held.Value.Bounds.extents.y), dropTarget.Value.TopmostDropTarget.transform.rotation, objectSpeed, rotationSpeed);
            held.Value.transform.parent = heldParent.Value;
            held = Maybe<InteractableObject>.Missing();
        }
        
        public static bool InteractActionTriggered()
        {
            return ControllerChecker.IsX() 
                   || Input.GetKeyDown(KeyCode.E) 
                   || Input.GetMouseButtonDown(0);
        }

        public static bool IsDropActionTriggered() => InteractActionTriggered();

        private bool IsDropActionHeld()
        {
            return Input.GetKey("joystick button 0") 
                   || Input.GetKey(KeyCode.E) 
                   || Input.GetMouseButton(0);
        }
        
        public static bool IsThrowActionTriggered()
        {
            return ControllerChecker.IsB() 
                   || Input.GetKeyDown(KeyCode.Q) 
                   || Input.GetMouseButtonDown(1);
        }
        
        public static bool IsThrowActionHeld()
        {
            return ControllerChecker.IsBHeld()
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

        protected override void Execute(ObjectTeleported msg)
        {
            if (msg.Obj == held.Value)
            {
                held.Value.transform.parent = heldParent.Value;
                held = Maybe<InteractableObject>.Missing();
            }
        }
    }
}