using UnityEngine;

public class MapSize : MonoBehaviour
{
    public static MapSize instance;

    public GameObject gameMap;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public (float, float) GetMapDimensions()
    {
        var sr = gameMap.GetComponent<SpriteRenderer>();

        var spriteSize = sr.size;
        var scale = gameMap.transform.lossyScale;

        var mapWidth = spriteSize.x * scale.x;
        var mapHeight = spriteSize.y * scale.y;

        return (mapWidth, mapHeight);
    }
}