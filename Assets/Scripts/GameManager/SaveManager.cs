using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float[] position;
}

[Serializable]
public class EnemyData
{
    public string enemyType;
    public float[] position;
}

[Serializable]
public class ProjectileData
{
    public float[] position;
    public float[] rotation;
    public float[] velocity;

    public ProjectileData(GameObject projectile)
    {
        position[0] = projectile.transform.position.x;
        position[1] = projectile.transform.position.y;
        position[2] = projectile.transform.position.z;

        rotation[0] = projectile.transform.rotation.x;
        rotation[1] = projectile.transform.rotation.y;
        rotation[2] = projectile.transform.rotation.z;
        rotation[3] = projectile.transform.rotation.w;

        var rb = projectile.GetComponent<Rigidbody2D>();
        velocity[0] = rb.velocity.x;
        velocity[1] = rb.velocity.y;
    }
}

public class SaveManager : MonoBehaviour
{
    public void SaveGame()
    {
    }
}