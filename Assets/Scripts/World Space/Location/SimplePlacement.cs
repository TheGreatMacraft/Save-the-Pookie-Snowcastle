public class SimplePlacement : Placement
{
    private Movable movable;
    private Location destination;


    public SimplePlacement(
        Movable movable,
        Location destination
        )
    {
        this.movable = movable;
        this.destination = destination;
    }


    public void Place()
    {
        movable.MoveTo(destination);
    }
}