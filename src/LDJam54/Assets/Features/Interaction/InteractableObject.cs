using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Rigidbody Body;
    public Collider Collider;
    public bool CanBeHeld;
    public float DistanceInFront;
    public float HeightOffset;

    public void Hold()
    {
        Body.useGravity = false;
        Collider.isTrigger = true;
    } 
    
    public void Highlight() {}
    public void Unhighlight() {}
}