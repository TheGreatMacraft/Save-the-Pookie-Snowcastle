using System.Collections.Generic;
using UnityEngine;


// Instantiate

public interface ObjectBuilder<T>
{
    T Build(Location spawnLocation, Rotation spawnOrientation);
}

// -> Spawner (Adjacent to ObjectBuilder)
public interface Spawner
{
    void SpawnAt(Location spawnPoint, Rotation facingRotation);
}


// Collection & Read-only Collection

public interface ReadOnlyCollection<T>
{
    IEnumerable<T> AllElements();

    List<T> Copy()
        => new List<T>(AllElements());
    
    public int Count()
    {
        int count = 0;
        using var enumerator = AllElements().GetEnumerator();
        while (enumerator.MoveNext()) count++;
        return count;
    }
}

public interface Collection<T> :  ReadOnlyCollection<T>
{
    void Register(T element);
    void Unregister(T element);
}

// OnDestroy

public interface Terminable
{
    void Terminate();
}


// Transform, Rigidbody2D

public interface PhysicalBody :
    Location, Movable,
    RotationDefinition, Rotatable,
    VectorDefinition
{}

public interface PhysicalMovement :
    Force
{}
    
// Trigger-Collider Object-Detection
public interface ColliderSensor
{
    void Connect(ColliderListener listener);
    void Disconnect(ColliderListener listener);
    ReadOnlyCollection<GameObject> ObjectsInCollider();
}

public interface ColliderListener
{
    void OnEnter(GameObject other);
}

// Sprite Renderer
public interface Perspective
{
    public void SetDepth();
}

// Condition
public interface Condition
{
    public bool IsMet();
}

// Disablable
public interface Disablable
{
    public bool IsEnabled();
    public void Disable();
    public void Enable();
}