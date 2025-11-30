using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveHandling;

// Enemy Group ------------

[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab;
    public int groupCount;
    public int spawnedEnemies;
    public (float, float) spawnTimerRange;
    public bool finishedSpawning;

    public EnemyGroup(string enemyTypeName, int enemyCount)
    {
        EnemyType enemyType = EnemyTypes.instance.GetTypeByName(enemyTypeName);

        this.enemyPrefab = enemyType.enemyPrefab;
        this.groupCount = enemyCount;
        this.spawnTimerRange = (enemyType.spawnTimerRange.min, enemyType.spawnTimerRange.max);

        this.finishedSpawning = false;
        this.spawnedEnemies = 0;
    }

    public float GetSpawnTime() { return UnityEngine.Random.Range(spawnTimerRange.Item1, spawnTimerRange.Item2); }

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
        graceTime = UnityEngine.Random.Range(graceTimeRange.Item1,graceTimeRange.Item2);
    }
}

// Wave Handling ------------

public class WaveHandling : MonoBehaviour
{
    public static WaveHandling instance;

    private TextAsset jsonFile;
    public Wave[] waves;

    public event Action<bool> onWaveStateChanged;

    public bool _isWave;
    public bool isWave
    {
        get => _isWave;
        set
        {
            if (_isWave != value)
            {
                _isWave = value;
                onWaveStateChanged?.Invoke(_isWave);
            }
        }
    }

    //Coroutine Handling ------------
    public IEnumerator StartWave(int waveIndex)
    {
        isWave = true;
        Wave wave = waves[waveIndex];
        
        foreach (Subwave subwave in wave.subwaves)
        {
            yield return StartCoroutine(StartSubwave(subwave));
        }
        EndWave(waveIndex);
    }

    public IEnumerator StartSubwave(Subwave subwave)
    {
        foreach (var group in subwave.enemyGroups)
            StartCoroutine(group.SpawnEnemyGroup());

        while (!subwave.AllFinishedSpawning() || !EnemyTracker.instance.anyElementsRegistred())
            yield return null;

    }

    private void EndWave(int waveIndex)
    {
        isWave = false;
        Debug.Log("Wave Finished!");

        StartCoroutine(StartWaveAfterDelay(waveIndex + 1, waves[waveIndex].graceTime));
    }

    private IEnumerator StartWaveAfterDelay(int nextWaveIndex, float minutes)
    {
        yield return new WaitForSeconds(minutes * 60);
        yield return StartCoroutine(StartWave(nextWaveIndex));
    }

    private Wave[] LoadWavesFromFile()
    {
        List<Wave> waves = new List<Wave>();

        jsonFile = Resources.Load<TextAsset>("JSON_data/wave_data");
        WaveList data = JsonUtility.FromJson<WaveList>(jsonFile.text);

        foreach(var wave in data.waves)
        {
            List<Subwave> subwaves = new List<Subwave>();
            foreach(var subwave in wave.subwaves)
            {
                List<EnemyGroup> enemyGroups = new List<EnemyGroup>();
                foreach(var enemyGroup in subwave.enemyGroups)
                {
                    enemyGroups.Add(new EnemyGroup(enemyGroup.enemyType, enemyGroup.enemyCount));
                }
                subwaves.Add(new Subwave(enemyGroups.ToArray()));
            }
            waves.Add(new Wave(subwaves.ToArray(),(wave.graceTimeRange.min,wave.graceTimeRange.max)));
        }

        return waves.ToArray();
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;

        isWave = false;
        waves = LoadWavesFromFile();
    }

    private void Start()
    {
        StartCoroutine(StartWave(0));
    }

    // Json Helper Classes

    [System.Serializable]
    public class FloatRange
    {
        public float min;
        public float max;
    }

    [System.Serializable]
    public class enemyGroupJson
    {
        public string enemyType;
        public int enemyCount;
    }

    [System.Serializable]
    public class SubwaveJson
    {
        public enemyGroupJson[] enemyGroups;
    }

    [System.Serializable]
    public class WaveJson
    {
        public SubwaveJson[] subwaves;
        public FloatRange graceTimeRange;
    }

    [System.Serializable]
    public class WaveList
    {
        public WaveJson[] waves;
    }
}
