using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSize : MonoBehaviour
{
    public static MapSize instance;

    public GameObject gameMap;

    public (float, float) GetMapDimensions()
    {
        SpriteRenderer sr = gameMap.GetComponent<SpriteRenderer>();

        Vector2 spriteSize = sr.size;
        Vector3 scale = gameMap.transform.lossyScale;

        float mapWidth = spriteSize.x * scale.x;
        float mapHeight = spriteSize.y * scale.y;

        return (mapWidth, mapHeight);
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
}
