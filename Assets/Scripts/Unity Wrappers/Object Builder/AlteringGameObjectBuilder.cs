using UnityEngine;

public class AlteringGameObjectBuilder<T> where T : MonoBehaviour
{
    private readonly T prefab;
    
    
    public AlteringGameObjectBuilder(
        T prefab
    )
    {
        this.prefab = prefab;
    }
    

    public T Build(Location spawnLocation, Rotation spawnOrientation)
    {
        return Object.Instantiate(
            prefab,
            spawnLocation.Coordinates(),
            spawnOrientation.Quaternion()
            );
    }
}