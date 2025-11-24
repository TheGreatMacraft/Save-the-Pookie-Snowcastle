using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy Group ------------

[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab;
    public int groupCount;
    public int spawnedEnemies;
    public (float, float) spawnTimerRange;
    public bool finishedSpawning;

    public EnemyGroup(GameObject enemyPrefab, int enemyCount, (float,float) spawnTimerRange)
    {
        this.enemyPrefab = enemyPrefab;
        this.groupCount = enemyCount;
        this.spawnedEnemies = 0;
        this.spawnTimerRange = spawnTimerRange;
        this.finishedSpawning = false;
    }

    public float GetSpawnTime() { return Random.Range(spawnTimerRange.Item1,spawnTimerRange.Item2);  }

    public IEnumerator SpawnEnemyGroup()
    {
        while (spawnedEnemies < groupCount)
        {
            yield return new WaitForSeconds(GetSpawnTime());
            Spawner.instance.Spawn(enemyPrefab);
            spawnedEnemies++;
        }
        finishedSpawning = true;
    }
}

// Subwave ------------

[System.Serializable]
public class Subwave
{
    public EnemyGroup[] enemyGroups;

    public Subwave(EnemyGroup[] enemyGroups)
    {
        this.enemyGroups = enemyGroups;
    }

    public bool AllFinishedSpawning()
    {
        foreach (var group in enemyGroups)
        {
            if(!group.finishedSpawning) return false;
        }

        return true;
    }
}

// Wave ------------

public class Wave
{
    public Subwave[] subwaves;
    public float graceTime;
    public Wave(Subwave[] subwaves, (float, float) graceTimeRange)
    {  
        this.subwaves = subwaves;
        graceTime = Random.Range(graceTimeRange.Item1,graceTimeRange.Item2);
    }
}

// Wave Handling ------------

public class WaveHandling : MonoBehaviour
{
    public static WaveHandling instance;

    //Coroutine Handling ------------
    public IEnumerator StartWave(Wave wave)
    {
        foreach (Subwave subwave in wave.subwaves)
        {
            yield return StartCoroutine(StartSubwave(subwave));
        }
        EndWave(wave);
    }

    public IEnumerator StartSubwave(Subwave subwave)
    {
        foreach (var group in subwave.enemyGroups)
            StartCoroutine(group.SpawnEnemyGroup());

        while (!subwave.AllFinishedSpawning())
            yield return null;

        while(!EnemyTracker.instance.anyAlive())
            yield return null;
    }

    private void EndWave(Wave wave)
    {
        Debug.Log("Wave Finished!");
    }

    private Wave SetupWave()
    {
        EnemyGroup[] enemyGroups = new EnemyGroup[]
        {
            new EnemyGroup(PrefabManager.instance.enemy,12,(5,8)),
            new EnemyGroup(PrefabManager.instance.snowman,3,(10,12))
        };

        Subwave[] subwave = new Subwave[]
        {
            new Subwave(enemyGroups),
        };

        return new Wave(subwave, (30, 50));
    }

    public Wave firstWave;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        firstWave = SetupWave();
    }

    private void Start()
    {
        StartCoroutine(StartWave(firstWave));
    }
}
