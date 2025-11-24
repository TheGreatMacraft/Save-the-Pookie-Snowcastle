using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;

    public GameObject enemy;
    public GameObject snowman;

    public GameObject snowball;

    private void LoadAllPrefabs()
    {
        enemy = Resources.Load<GameObject>("Prefabs/Enemies/Enemy");
        snowman = Resources.Load<GameObject>("Prefabs/Enemies/Snowman");

        snowball = Resources.Load<GameObject>("Prefabs/Projectiles/Snowball");
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        LoadAllPrefabs();
    }
}
