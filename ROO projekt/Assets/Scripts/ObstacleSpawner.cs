using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; 
    public float spawnInterval = 1.2f; 
    public float spawnY = 10f;
    private float[] lanes = { -3f, 0f, 3f }; 
    private int waveCount = 0; // Tracks number of waves spawned

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
    while (true)
        {
            waveCount++;

            if (waveCount % 5 == 0)
            {
                // Every 5th wave, spawn 3 obstacles (one in each lane)
                SpawnThreeObstacles();
            }
            else
            {
                // Otherwise, spawn 2 obstacles in different lanes
                SpawnTwoObstacles();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnTwoObstacles()
    {
        // Pick two different lanes
        int firstLaneIndex = Random.Range(0, lanes.Length);
        int secondLaneIndex;
        do
        {
            secondLaneIndex = Random.Range(0, lanes.Length);
        } while (secondLaneIndex == firstLaneIndex); // Ensure they are different

        // Spawn obstacles in the chosen lanes
        SpawnObstacle(lanes[firstLaneIndex]);
        SpawnObstacle(lanes[secondLaneIndex]);
    }

    private void SpawnThreeObstacles()
    {
        foreach (float lane in lanes)
        {
            SpawnObstacle(lane);
        }
    }

    private void SpawnObstacle(float spawnX)
    {
        int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstaclePrefab = obstaclePrefabs[randomObstacleIndex];

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
