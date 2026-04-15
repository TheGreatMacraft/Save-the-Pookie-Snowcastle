using UnityEngine;

public sealed class OffsetLocation 
    : Location
{
    private readonly Location origin;
    private readonly ReadOnlyCollection<Offset> offsets;


    public OffsetLocation(Location origin, ReadOnlyCollection<Offset> offsets)
    {
        this.origin = origin;
        this.offsets = offsets;
    }

    public Vector3 Coordinates()
    {
        Vector3 coordinates = origin.Coordinates();

        foreach (Offset offset in offsets.AllElements())
        {
            coordinates += offset.Coordinates();
        }
        
        return coordinates;
    }
}