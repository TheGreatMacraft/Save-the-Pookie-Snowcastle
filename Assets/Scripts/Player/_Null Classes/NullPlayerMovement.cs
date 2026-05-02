public sealed class NullPlayerMovement : PlayerMovement
{
    private ActionExecution nullActionExecution = new NullActionExecution();

    public ActionExecution RollAction() => nullActionExecution;
    public ActionExecution Movement() => nullActionExecution;
}