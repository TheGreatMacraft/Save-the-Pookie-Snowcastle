using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSnowball : MonoBehaviour
{
    public GameObject map;

    public float hitDamage = 25;

    float mapWidth;
    float mapHeight;

    private void SetMapDimensions(GameObject gameMap)
    {
        SpriteRenderer sr = gameMap.GetComponent<SpriteRenderer>();

        Vector2 spriteSize = sr.size;
        Vector3 scale = gameMap.transform.lossyScale;

        mapWidth = spriteSize.x * scale.x;
        mapHeight = spriteSize.y * scale.y;
    }

    private bool OutOfBounds()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        return x < -mapWidth/2 || x > mapWidth/2 || y < -mapHeight/2 || y > mapHeight/2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<EnemyHealth>().DecreaseHelth(hitDamage);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        map = GameObject.Find("MapBackground");

        SetMapDimensions(map);
    }

    private void Update()
    {
        if (OutOfBounds())
        {
            Destroy(gameObject);
        }
    }
}
