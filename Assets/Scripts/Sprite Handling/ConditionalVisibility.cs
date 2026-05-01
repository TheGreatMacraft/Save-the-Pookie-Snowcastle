public sealed class ConditionalVisibility
    : Presentation
{
    private readonly Visibility visibility;
    private readonly Condition condition;


    public ConditionalVisibility(Visibility visibility, Condition condition)
    {
        this.visibility = visibility;
        this.condition = condition;
    }


    public void Present()
    {
        if(condition.IsMet() && !visibility.IsVisible().IsMet())
            visibility.Show();
        else if(!condition.IsMet() && visibility.IsVisible().IsMet())
            visibility.Hide();
    }
}