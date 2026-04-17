

// Manipulators  -  usually called in MonoBehaviours 

public interface Movement
{
    void Move();
}

public interface ComplexMovement 
    : Movement, Condition
{
    public Condition IsMoving() => this;
}

public interface Placement
{
    void Place();
}

public interface Orientation
{
    void Orient();
}


// Tag

public interface Tagged
{
    bool IsTaggedAs(string Tag);
}

public interface Filter<T>
{
    T Value();
    bool Passed();
}


// Target

public interface Target : 
    Location, Tagged
{
    void Hit(Impact impact, Terminable disposableHitter);
}

public interface TargetSource
{
    Target CurrentTarget();
}

public interface TargetLocationSource : Location {}

public interface TargetScouter : TargetSource
{
    void FindNewTarget();
}