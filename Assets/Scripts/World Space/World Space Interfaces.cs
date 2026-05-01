using UnityEngine;


// Location

public interface Location
{
    Vector3 Coordinates();
    
    bool Equals(Location other)
        => Coordinates().Equals(other.Coordinates());
}

public interface Movable
{
    void MoveTo(Location newLocation);
}

public interface ZCoordinate : Scalar<float> {}

// Rotation

public interface RotationDefinition
{
    public Quaternion Quaternion();
}

public interface Rotatable
{
    void RotateAs(Rotation newOrientation);
}


// Vector

public interface VectorDefinition
{
    Vector3 RawVector();
    Location StartLocation();
}


// Force

public interface Force
{
    void AddConstant(Vector direction, float speed);
    void AddImpulse(Vector direction, float amount);
    
    void SetForce(Vector direction, float amount);
    void ResetForce();
    void Stun(float duration);
}