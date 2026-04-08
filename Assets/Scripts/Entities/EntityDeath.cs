using UnityEngine;

public class EntityDeath : MonoBehaviour, Mortal
{
    public void Die()
    {
        Debug.Log("Killing entity: " + gameObject.name);
        
        Destroy(gameObject);
    }
}