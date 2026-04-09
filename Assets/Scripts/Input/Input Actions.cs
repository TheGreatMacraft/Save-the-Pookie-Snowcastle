// All Action Names MUST Correspond with those is Input System

public sealed class PrimaryInputAction : InputAction {
    public string ToString() => "Primary";
}

public sealed class SupportInputAction : InputAction {
    public string ToString() => "Support";
}

public sealed class SpecialInputAction : InputAction {
    public string ToString() => "Special";
}

public sealed class InteractInputAction : InputAction {
    public string ToString() => "Interact";
}

public sealed class RollInputAction : InputAction {
    public string ToString() => "Roll";
}