// All Action Names MUST Correspond with those is Input System

public sealed class PrimaryInputAction : InputAction {
    public string ToString() => "Primary";
}

public sealed class SecondaryInputAction : InputAction {
    public string ToString() => "Secondary";
}

public sealed class AbilityInputAction : InputAction {
    public string ToString() => "Ability";
}

public sealed class ToolInputAction : InputAction {
    public string ToString() => "Tool";
}

public sealed class RollInputAction : InputAction {
    public string ToString() => "Roll";
}