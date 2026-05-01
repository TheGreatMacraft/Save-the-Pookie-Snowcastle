public sealed class NullState : State
{
    public string Name() => "";
    public override bool Equals(object obj) => false;
}