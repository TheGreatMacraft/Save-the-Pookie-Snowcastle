public class NullActionExecution : ActionExecution
{
    private Condition trueCondition = new TrueCondition();
    private Condition falseCondition = new FalseCondition();

    public void Execute() {}
    public Condition InProgress() => falseCondition;
    public Condition Concluded() => trueCondition;
}