public sealed class NullPlayerMovement : PlayerMovement
{
    private ActionExecution nullActionExecution = new NullActionExecution();
    private Condition falseCondition = new FalseCondition();
    private Condition trueCondition = new TrueCondition();

    public ActionExecution RollAction() => nullActionExecution;
    public Condition IsMoving() => falseCondition;
    public Condition IsRolling() => trueCondition;
}