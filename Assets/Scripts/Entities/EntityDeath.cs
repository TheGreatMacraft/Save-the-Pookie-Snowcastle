using UnityEngine;

public class EntityDeath : MonoBehaviour, Mortal
{
    public void Die()
    {
        Debug.Log("Killing " + this.gameObject.name);
        
        Destroy(gameObject);
    }
}