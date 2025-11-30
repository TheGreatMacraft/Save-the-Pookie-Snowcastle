using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnemyTypes;

// Json Helper Classes

[System.Serializable]
public class FloatRange
{
    public float min;
    public float max;
}

[System.Serializable]
public class EnemyTypeJson
{
    public string typeName;
    public FloatRange spawnTimeRange;
}

[System.Serializable]
public class EnemyTypeList
{
    public EnemyTypeJson[] enemyTypes;
}


// Enemy Type
[System.Serializable]
public class EnemyType
{
    public GameObject enemyPrefab;
    public FloatRange spawnTimerRange;

    public EnemyType(string enemyTypeName, FloatRange spawnTimerRange)
    {
        this.enemyPrefab = PrefabManager.instance.GetPrefabByName(enemyTypeName);
        this.spawnTimerRange = spawnTimerRange;
    }
}


//Enemy Type Handler
public class EnemyTypes : MonoBehaviour
{
    public static EnemyTypes instance;

    public Dictionary<string, EnemyType> enemyTypeDict;

    private TextAsset jsonFile;

    private void ReadEnemyTypeDictFromFile()
    {
        jsonFile = Resources.Load<TextAsset>("JSON_data/enemy_type_data");
        EnemyTypeList data = JsonUtility.FromJson<EnemyTypeList>(jsonFile.text);

        enemyTypeDict = new Dictionary<string, EnemyType>();

        foreach (var enemyType in data.enemyTypes)
        {
            EnemyType type = new EnemyType(enemyType.typeName, enemyType.spawnTimeRange);
            enemyTypeDict[enemyType.typeName] = type;
        }
    }

    public EnemyType GetTypeByName(string name)
    {
        return enemyTypeDict[name];
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;

        ReadEnemyTypeDictFromFile();
    }
}
