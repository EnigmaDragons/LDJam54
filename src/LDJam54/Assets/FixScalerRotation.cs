using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScalerRotation : MonoBehaviour
{
    public GameObject Scaler;
    public GameObject Body;
    public GameObject Eyes;

    public void Start()
    {
        var rotation = Scaler.transform.eulerAngles;
        rotation.x = 0;
        Scaler.transform.eulerAngles = rotation;
        rotation = Body.transform.eulerAngles;
        rotation.x = 0;
        Body.transform.eulerAngles = rotation;
        rotation = Eyes.transform.eulerAngles;
        rotation.x = 0;
        Eyes.transform.eulerAngles = rotation;
    }
}
