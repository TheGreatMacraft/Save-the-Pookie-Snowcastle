using UnityEngine;


// -> Spawner (Adjacent to ObjectBuilder)
public interface Spawner
{
    void SpawnAt(Location spawnPoint, Rotation facingRotation);
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