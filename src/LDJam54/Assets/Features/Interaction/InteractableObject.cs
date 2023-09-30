using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Rigidbody Body;
    public Collider Collider;
    //Hold Details
    public bool CanBeHeld;
    public float DistanceInFront;
    public float HeightOffset;
    //Set On Details
    public bool CanBeSetOn;
    public bool SnapToCenter;

    [field: SerializeField] public float BobbingIntensity { get; private set; } = 0.05f;
    
    
    private bool _isSettingDown;
    private bool _isHeld;
    private float _setDownSpeed;
    private Vector3 _setDownDestination;
    private float _setDownRotationSpeed;
    private Quaternion _setDownRotation;
    
    private float yOffset;
    private void FixedUpdate()
    {
        if (!_isSettingDown)
            return;
        Body.MoveRotation(Quaternion.RotateTowards(transform.rotation, _setDownRotation, _setDownRotationSpeed * Time.fixedDeltaTime));
        Body.MovePosition(Vector3.MoveTowards(transform.position, _setDownDestination, _setDownSpeed * Time.fixedDeltaTime));
        if (Vector3.Distance(Body.position, _setDownDestination) < 0.01f)
        {
            _isSettingDown = false;
            Body.useGravity = true;
        }
    }


    public void Hold()
    {
        _isSettingDown = false;
        _isHeld = true;
        Body.useGravity = false;
        Collider.enabled = false;
    }

    public void SetDown(Vector3 destination, Quaternion rotation, float speed, float rotationSpeed)
    {
        Collider.enabled = true;
        _isSettingDown = true;
        _isHeld = false;
        _setDownDestination = destination;
        _setDownRotation = rotation;
        _setDownSpeed = speed;
        _setDownRotationSpeed = rotationSpeed;
    }
    
    public void Highlight() {}
    public void Unhighlight() {}
}