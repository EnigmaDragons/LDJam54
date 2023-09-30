using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float pickupSpeed = 1;
    [SerializeField] private float pickupRotationSpeed = 1;
    [SerializeField] private Camera camera;
    private Maybe<InteractableObject> targetted = Maybe<InteractableObject>.Missing();
    private Maybe<InteractableObject> held = Maybe<InteractableObject>.Missing();
    
    private void Update()
    {
        UpdateTarget();
        CheckForPickup();
    }

    private void FixedUpdate()
    {
        UpdateHeldObject();
    }

    private void UpdateTarget()
    {
        var ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out var hit))
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
    
    private void CheckForPickup()
    {
        if (held.IsMissing && targetted.IsPresent && targetted.Value.CanBeHeld && (Input.GetKeyDown("joystick button 0") || Input.GetMouseButtonDown(0)))
        {
            held = targetted.Value;
            targetted.Value.Hold();
            targetted.Value.transform.parent = transform;
        }
    }

    private void UpdateHeldObject()
    {
        held.IfPresent(x =>
        {
            x.Body.MoveRotation(Quaternion.RotateTowards(x.transform.rotation, transform.rotation, pickupRotationSpeed * Time.fixedDeltaTime));
            x.Body.MovePosition(Vector3.MoveTowards(x.transform.position, transform.position + transform.forward * x.DistanceInFront + new Vector3(0, x.HeightOffset, 0), pickupSpeed * Time.fixedDeltaTime));
        });
    }
}