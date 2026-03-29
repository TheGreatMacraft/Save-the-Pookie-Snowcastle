using UnityEngine;

public class NullColliderSensor : 
    ColliderSensor
{
    public void Connect(ColliderListener listener) {}
    public void Disconnect(ColliderListener listener) {}

    public ReadOnlyCollection<GameObject> ObjectsInCollider()
        => new NullCollection<GameObject>();
}