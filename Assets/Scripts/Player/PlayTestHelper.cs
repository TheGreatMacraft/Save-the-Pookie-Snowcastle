using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayTestHelper : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            KillAllEnemies();
    }

    private void KillAllEnemies()
    {
        List<GameObject> enemiesList = new List<GameObject>(EnemyTracker.instance.registeredElements);
        
        foreach (var enemy in enemiesList)
        {
            enemy.GetComponent<EnemyHealth>().Die();
        }
    }
}