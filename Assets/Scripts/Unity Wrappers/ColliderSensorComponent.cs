using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]

public sealed class ColliderSensorComponent :
    MonoBehaviour, 
    ColliderSensor
{
    private Collection<ColliderListener> listeners 
        = new SimpleCollection<ColliderListener>();
    
    private Collection<GameObject> objectsInCollider;

    
    public void Connect(ColliderListener listener)
    {
        listeners.Register(listener);
    }

    public void Disconnect(ColliderListener listener)
    {
        listeners.Unregister(listener);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var listener in listeners.AllElements())
            listener.OnEnter(other.gameObject);
        
        objectsInCollider.Register(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        objectsInCollider.Unregister(other.gameObject);
    }

    public ReadOnlyCollection<GameObject> ObjectsInCollider()
        => objectsInCollider;
}