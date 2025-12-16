using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Enemy Group ------------

[Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab;
    public int groupCount;
    public int spawnedEnemies;
    public bool finishedSpawning;
    public (float, float) spawnTimerRange;

    public EnemyGroup(string enemyTypeName, int enemyCount)
    {
        var enemyType = EnemyTypes.instance.GetTypeByName(enemyTypeName);

        enemyPrefab = enemyType.enemyPrefab;
        groupCount = enemyCount;
        spawnTimerRange = (enemyType.spawnTimerRange.min, enemyType.spawnTimerRange.max);

        finishedSpawning = false;
        spawnedEnemies = 0;
    }

    public float GetSpawnTime()
    {
        return Random.Range(spawnTimerRange.Item1, spawnTimerRange.Item2);
    }

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

[Serializable]
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
            if (!group.finishedSpawning)
                return false;

        return true;
    }
}

// Wave ------------

public class Wave
{
    public float graceTime;
    public Subwave[] subwaves;

    public Wave(Subwave[] subwaves, (float, float) graceTimeRange)
    {
        this.subwaves = subwaves;
        graceTime = Random.Range(graceTimeRange.Item1, graceTimeRange.Item2);
    }
}

// Wave Handling ------------

public class WaveHandling : MonoBehaviour
{
    public static WaveHandling instance;

    public bool _isWave;

    private TextAsset jsonFile;
    public Wave[] waves;

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

    private void Awake()
    {
        if (instance == null)
            instance = this;

        isWave = false;
        waves = LoadWavesFromFile();
    }

    private void Start()
    {
        StartCoroutine(StartWave(0));
    }

    public event Action<bool> onWaveStateChanged;

    //Coroutine Handling ------------
    public IEnumerator StartWave(int waveIndex)
    {
        isWave = true;
        var wave = waves[waveIndex];

        foreach (var subwave in wave.subwaves) yield return StartCoroutine(StartSubwave(subwave));
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
        var waves = new List<Wave>();

        jsonFile = Resources.Load<TextAsset>("JSON_data/wave_data");
        var data = JsonUtility.FromJson<WaveList>(jsonFile.text);

        foreach (var wave in data.waves)
        {
            var subwaves = new List<Subwave>();
            foreach (var subwave in wave.subwaves)
            {
                var enemyGroups = new List<EnemyGroup>();
                foreach (var enemyGroup in subwave.enemyGroups)
                    enemyGroups.Add(new EnemyGroup(enemyGroup.enemyType, enemyGroup.enemyCount));
                subwaves.Add(new Subwave(enemyGroups.ToArray()));
            }

            waves.Add(new Wave(subwaves.ToArray(), (wave.graceTimeRange.min, wave.graceTimeRange.max)));
        }

        return waves.ToArray();
    }

    // Json Helper Classes

    [Serializable]
    public class FloatRange
    {
        public float min;
        public float max;
    }

    [Serializable]
    public class enemyGroupJson
    {
        public string enemyType;
        public int enemyCount;
    }

    [Serializable]
    public class SubwaveJson
    {
        public enemyGroupJson[] enemyGroups;
    }

    [Serializable]
    public class WaveJson
    {
        public SubwaveJson[] subwaves;
        public FloatRange graceTimeRange;
    }

    [Serializable]
    public class WaveList
    {
        public WaveJson[] waves;
    }
}