using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;

    public GameObject gingerbreadman;
    public GameObject snowman;
    public GameObject gnome;
    public GameObject snow_golem;

    public GameObject snowball;

    private Dictionary<string, GameObject> prefabDict;

    private void LoadAllPrefabs()
    {
        gingerbreadman = Resources.Load<GameObject>("Prefabs/Enemies/Gingerbreadman");
        snowman = Resources.Load<GameObject>("Prefabs/Enemies/Snowman");
        gnome = Resources.Load<GameObject>("Prefabs/Enemies/Gnome");
        snow_golem = Resources.Load<GameObject>("Prefabs/Enemies/Snow Golem");

        snowball = Resources.Load<GameObject>("Prefabs/Projectiles/Snowball");
    }

    private void FillPrefabDict()
    {
        prefabDict = new Dictionary<string, GameObject>
        {
            {"gingerbreadman", gingerbreadman },
            {"snowman", snowman },
            {"gnome", gnome },
            {"snow golem", snow_golem },

            {"snowball", snowball}
        };
    }

    //Pubic Functions for Getting Prefabs
    public GameObject GetPrefabByName(string name)
    {
        return prefabDict[name];
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        LoadAllPrefabs();
        FillPrefabDict();
    }
}
