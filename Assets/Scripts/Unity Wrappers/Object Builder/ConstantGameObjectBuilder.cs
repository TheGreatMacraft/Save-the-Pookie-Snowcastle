using UnityEngine;

public class ConstantGameObjectBuilder<T> where T : MonoBehaviour
{
    private readonly T prefab;
    private readonly Location spawnLocation;
    private readonly Rotation spawnOrientation;


    public ConstantGameObjectBuilder(T prefab, PhysicalBody physicalBody)
        : this(prefab, physicalBody, new Rotation(physicalBody)) {}
    
    public ConstantGameObjectBuilder(
        T prefab,
        Location spawnLocation,
        Rotation spawnOrientation
    )
    {
        this.prefab = prefab;
        this.spawnLocation = spawnLocation;
        this.spawnOrientation = spawnOrientation;
    }
    

    public T Build()
    {
        return Object.Instantiate(
            prefab,
            spawnLocation.Coordinates(),
            spawnOrientation.Quaternion()
            );
    }
}