public sealed class NullPlayerMovement : PlayerMovement
{
    private ActionExecution nullActionExecution = new NullActionExecution();

    public ActionExecution RollAction() => nullActionExecution;
    public bool IsMoving() => false;
}