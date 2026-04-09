public sealed class ReloadCall 
    : ActionCall
{
    private readonly Magazine magazine;


    public ReloadCall(Magazine magazine)
    {
        this.magazine = magazine;
    }
    
    
    public void Call()
    {
        magazine.Restore();
    }
}