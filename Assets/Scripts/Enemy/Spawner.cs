using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    public GameObject map;

    private Vector2 spawnPoint;
    private Vector3 newEnemyPosition;

    private float spawnCooldown = 5f;

    private Vector2 GetRandomSpawnPoint(GameObject gameMap)
    {
        SpriteRenderer sr = gameMap.GetComponent<SpriteRenderer>();

        Vector2 spriteSize = sr.size;
        Vector3 scale = gameMap.transform.lossyScale;

        float width = spriteSize.x * scale.x;
        float height = spriteSize.y * scale.y;

        Vector2 center = gameMap.transform.position;

        bool verticalSide = Random.value < 0.5f;

        if (verticalSide)
        {
            float x = (Random.value < 0.5f)
                ? center.x - width / 2
                : center.x + width / 2;

            float y = Random.Range(center.y - height / 2, center.y + height / 2);
            return new Vector2(x, y);
        }
        else
        {
            float y = (Random.value < 0.5f)
                ? center.y - height / 2
                : center.y + height / 2;

            float x = Random.Range(center.x - width / 2, center.x + width / 2);
            return new Vector2(x, y);
        }
    }

    private void Start()
    {
        map = GameObject.Find("MapBackground");

        InvokeRepeating("SpawnInvoke",0f, spawnCooldown);
    }

    private void SpawnInvoke() { Spawn(enemy); }

    private void Spawn(GameObject enemy)
    {
        spawnPoint = GetRandomSpawnPoint(map);
        newEnemyPosition = new Vector3(spawnPoint.x, spawnPoint.y, 0f);
        Instantiate(enemy, newEnemyPosition, Quaternion.identity);
    }
}
