using System;

public sealed class BasicMagazine : Magazine
{
    private readonly int magazineSize;
    private int ammoLeft;


    public BasicMagazine(int MagazineSize)
        : this(MagazineSize, MagazineSize) {}

    private BasicMagazine(int MagazineSize, int ammoLeft)
    {
        this.magazineSize = MagazineSize;
        this.ammoLeft = ammoLeft;
    }
    
    
    public bool IsEmpty() => ammoLeft == 0;

    public void SpendAmmo(int amount)
    {
        ammoLeft = Math.Max(ammoLeft - amount, 0);
    }

    public void Restore()
    {
        ammoLeft = magazineSize;
    }
}