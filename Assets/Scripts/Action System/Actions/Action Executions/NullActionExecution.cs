public class NullActionExecution : ActionExecution
{
    private readonly Condition nullCondition = new TrueCondition();
    
    public void Execute() {}
    public bool IsMet() => nullCondition.IsMet();
}