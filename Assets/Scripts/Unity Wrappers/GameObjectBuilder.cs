using UnityEngine;

public class GameObjectBuilder<T> : 
    ObjectBuilder<T> where T : MonoBehaviour
{
    private readonly T prefab;
    
    
    public GameObjectBuilder(T prefab)
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