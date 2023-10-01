using UnityEngine;

public class ObjectPickedUp
{
    public ObjectType ObjectType;
    public Vector3 Position;

    public ObjectPickedUp(Transform obj, ObjectType type)
    {
        ObjectType = type;
        Position = obj.position;
    }
}
