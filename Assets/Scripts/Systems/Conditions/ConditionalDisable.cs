public sealed class ConditionalDisable
{
    private readonly Disablable disablable;
    private readonly Condition condition;


    public ConditionalDisable(
        Disablable disablable,
        Condition condition
    )
    {
        this.disablable = disablable;
        this.condition = condition;
    }

    public void Check()
    {
        if(condition.IsMet() && disablable.IsEnabled())
            disablable.Disable();
        else if(!condition.IsMet() && !disablable.IsEnabled())
            disablable.Enable();
    }
}