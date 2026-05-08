

// Manipulators  -  usually called in MonoBehaviours 

public interface Movement : ActionCall
{
    public void Move();
    
    void ActionCall.Call() { Move(); }
}

public interface ComplexMovement 
    : Movement, Condition
{
    public Condition IsMoving() => this;
}

public interface Placement : ActionCall
{
    void Place();
    
    void ActionCall.Call() { Place(); }
}

public interface Orientation : ActionCall
{
    void Orient();
    
    void ActionCall.Call() { Orient(); }
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

public interface TargetPicker : Scalar<Target> {}

public interface TargetLocationSource : Location { }

// Speed
public interface Speed : Scalar<float> {}