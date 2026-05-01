public sealed class InfiniteMagazine : Magazine
{
    public bool IsEmpty() => false;
    public void SpendAmmo(int amount) {}
    public void Restore() {}
}