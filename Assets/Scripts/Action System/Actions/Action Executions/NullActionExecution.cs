public class NullActionExecution : ActionExecution
{
    public void Execute() {}
    public bool Concluded() => true;
}