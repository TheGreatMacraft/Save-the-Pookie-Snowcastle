// All Action Names MUST Correspond with those is Input System

public sealed class PrimaryInputAction : InputAction {
    public string ToString() => "Primary";
}

public sealed class SecondaryInputAction : InputAction {
    public string ToString() => "Secondary";
}

public sealed class MovementInputAction : InputAction {
    public string ToString() => "Movement";
}

public sealed class MovementSpecialInputAction : InputAction {
    public string ToString() => "Movement Special";
}

public sealed class PowerInputAction : InputAction {
    public string ToString() => "Power";
}

public sealed class SpecialInputAction : InputAction {
    public string ToString() => "Special";
}

public sealed class SupportInputAction : InputAction {
    public string ToString() => "Support";
}

public sealed class InteractInputAction : InputAction {
    public string ToString() => "Interact";
}

public sealed class BuildMenuInputAction : InputAction {
    public string ToString() => "Build Menu";
}