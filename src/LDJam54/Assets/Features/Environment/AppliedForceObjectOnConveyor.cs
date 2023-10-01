using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppliedForceObjectOnConveyor : MonoBehaviour
{
    [SerializeField] private Rigidbody body;

    private List<Tuple<GameObject, Vector3>> forceSources = new List<Tuple<GameObject, Vector3>>();

    private void Update()
    {
        if (body.useGravity && !body.isKinematic && forceSources.Count > 0)
            body.gameObject.transform.position += forceSources[0].Item2 * Time.deltaTime; //body.AddForce(forceSources[0].Item2, ForceMode.VelocityChange);
        else if (forceSources.Count > 0)
            forceSources.Clear();
    }

    public void AddForce(Vector3 force, GameObject owner)
    {
        forceSources.Add(new Tuple<GameObject, Vector3>(owner, force));
    }

    public void RemoveForce(GameObject owner)
    {
        forceSources.RemoveAll(x => x.Item1 == owner);
    }
}