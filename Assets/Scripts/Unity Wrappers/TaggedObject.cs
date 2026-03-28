public sealed class TaggedObject : 
    Tagged
{
    private readonly string tag;
    
    public TaggedObject(string tag)
    {
        this.tag = tag;
    }
    
    public bool IsTaggedAs(string checkTag) 
        => tag == checkTag;
}