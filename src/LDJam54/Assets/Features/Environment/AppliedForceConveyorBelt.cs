using System;
using UnityEngine;

public class AppliedForceConveyorBelt : MonoBehaviour
{
    [SerializeField] private FloatReference force;
    
    private void OnTriggerEnter(Collider other)
    {
        var component = other.GetComponent<AppliedForceObjectOnConveyor>();
        if (component != null)
            component.AddForce(force * transform.forward, gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        var component = other.GetComponent<AppliedForceObjectOnConveyor>();
        if (component != null)                                                    
            component.RemoveForce(gameObject);                                
    }
}