using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public GameObject obstaclePrefab; 
    public Sprite[] sprites;
    private float fallSpeed = 4f; 
    private float spawnInterval = 1f;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

    }

    void Update()
    {
        InvokeRepeating("SpawnObstacles", 0f, spawnInterval);
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void SpawnObstacles() {
        Instantiate(obstaclePrefab, new Vector3(Random.Range(-8f, 8f), transform.position.y, 0), transform.rotation);
    }
}