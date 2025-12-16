using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public GameObject map;
    private Vector3 newEnemyPosition;
    private Vector2 spawnPoint;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        map = GameObject.Find("MapBackground");
    }

    private Vector2 GetRandomSpawnPoint(GameObject gameMap)
    {
        var sr = gameMap.GetComponent<SpriteRenderer>();

        var spriteSize = sr.size;
        var scale = gameMap.transform.lossyScale;

        var width = spriteSize.x * scale.x;
        var height = spriteSize.y * scale.y;

        Vector2 center = gameMap.transform.position;

        var verticalSide = Random.value < 0.5f;

        if (verticalSide)
        {
            var x = Random.value < 0.5f
                ? center.x - width / 2
                : center.x + width / 2;

            var y = Random.Range(center.y - height / 2, center.y + height / 2);
            return new Vector2(x, y);
        }
        else
        {
            var y = Random.value < 0.5f
                ? center.y - height / 2
                : center.y + height / 2;

            var x = Random.Range(center.x - width / 2, center.x + width / 2);
            return new Vector2(x, y);
        }
    }

    public void Spawn(GameObject enemy)
    {
        spawnPoint = GetRandomSpawnPoint(map);
        newEnemyPosition = new Vector3(spawnPoint.x, spawnPoint.y, 0f);
        Instantiate(enemy, newEnemyPosition, Quaternion.identity);
    }
}