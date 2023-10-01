using UnityEngine;

public class ObjectSetDown
{
    public ObjectType ObjectType;
    public Vector3 Position;

    public ObjectSetDown(Transform obj, ObjectType type)
    {
        ObjectType = type;
        Position = obj.position;
    }
}
