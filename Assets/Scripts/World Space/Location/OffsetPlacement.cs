public class OffSetPlacement : Placement
{
    private Movable movable;
    private Location destination;
    private ReadOnlyCollection<Offset> offsets;


    public OffSetPlacement(
        Movable movable,
        Location destination,
        ReadOnlyCollection<Offset> offsets
        )
    {
        this.movable = movable;
        this.destination = destination;
        this.offsets = offsets;
    }


    public void Place()
    {
        movable.MoveTo(new OffsetLocation(destination, offsets));
    }
}