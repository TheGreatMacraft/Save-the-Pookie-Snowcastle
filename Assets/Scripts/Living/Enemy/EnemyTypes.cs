using System;
using System.Collections.Generic;
using UnityEngine;

// Json Helper Classes

[Serializable]
public class FloatRange
{
    public float min;
    public float max;
}

[Serializable]
public class EnemyTypeJson
{
    public string typeName;
    public FloatRange spawnTimeRange;
}

[Serializable]
public class EnemyTypeList
{
    public EnemyTypeJson[] enemyTypes;
}


// Enemy Type
[Serializable]
public class EnemyType
{
    public GameObject enemyPrefab;
    public FloatRange spawnTimerRange;

    public EnemyType(string enemyTypeName, FloatRange spawnTimerRange)
    {
        enemyPrefab = PrefabManager.instance.GetPrefabByName(enemyTypeName);
        this.spawnTimerRange = spawnTimerRange;
    }
}


//Enemy Type Handler
public class EnemyTypes : MonoBehaviour
{
    public static EnemyTypes instance;

    public Dictionary<string, EnemyType> enemyTypeDict;

    private TextAsset jsonFile;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        ReadEnemyTypeDictFromFile();
    }

    private void ReadEnemyTypeDictFromFile()
    {
        jsonFile = Resources.Load<TextAsset>("JSON_data/enemy_type_data");
        var data = JsonUtility.FromJson<EnemyTypeList>(jsonFile.text);

        enemyTypeDict = new Dictionary<string, EnemyType>();

        foreach (var enemyType in data.enemyTypes)
        {
            var type = new EnemyType(enemyType.typeName, enemyType.spawnTimeRange);
            enemyTypeDict[enemyType.typeName] = type;
        }
    }

    public EnemyType GetTypeByName(string typeName)
    {
        return enemyTypeDict[typeName];
    }
}