using System.Collections.Generic;

public sealed class ClosestLocationIndex : 
    Index
{
    private readonly Location origin;
    private IEnumerable<Location> targets;
    
    private readonly Location nullLocation = new NullLocation();
    

    public ClosestLocationIndex(
        Location origin,
        IEnumerable<Location> targets
        )
    {
        this.origin = origin;
        this.targets = targets;
    }
    

    public int Value()
    {
        int winnerIndex = -1;
        float minDistance = float.MaxValue;

        int currentIndex = 0;
        foreach (Location potentialTarget in targets)
        {
            float currentDistance = new DistanceBetweenLocations(
                origin,
                potentialTarget
            ).Value();

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                winnerIndex = currentIndex;
            }

            currentIndex++;
        }
        
        return winnerIndex;
    }
}