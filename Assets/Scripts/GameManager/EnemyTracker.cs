using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public static EnemyTracker instance;

    List<GameObject> aliveEnemies = new List<GameObject>();

    public void RegisterEnemy(GameObject enemy)
    {
        aliveEnemies.Add(enemy);
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
    }

    public bool anyAlive() { return aliveEnemies.Count > 0; }

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }
}
