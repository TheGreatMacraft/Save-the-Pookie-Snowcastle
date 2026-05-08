public sealed class PlayerSlot : Index
{
    private readonly int maxSlotIndex;
    private int currentSlotIndex = 0;
    
    
    public PlayerSlot(int maxSlotIndex)
    {
        this.maxSlotIndex = maxSlotIndex;
    }
    
    
    public int Value() => currentSlotIndex;

    public void SetTo(int value) => currentSlotIndex = value;
    
    public void Increment()
    {
        if(currentSlotIndex < maxSlotIndex)
            currentSlotIndex++;
    }

    public void Decrement()
    {
        if(currentSlotIndex > 0)
            currentSlotIndex--;
    }
}