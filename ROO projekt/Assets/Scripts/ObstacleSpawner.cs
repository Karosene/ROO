using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // Regular obstacles
    public GameObject trainPrefab;        // Train segment prefab (assign in Inspector)

    public float spawnInterval = 1.2f;    
    public float minTrainSpawnTime = 5f;  // Minimum time between train spawns
    public float maxTrainSpawnTime = 10f; // Maximum time between train spawns
    public float trainLaneBlockTime = 3f; // Time to block lane after train spawns
    public float spawnY = 10f;
    
    private float[] lanes = { -3f, 0f, 3f }; 
    private int waveCount = 0; // Tracks number of waves spawned
    private HashSet<float> blockedLanes = new HashSet<float>(); // Track blocked lanes

    void Start()
    {
        StartCoroutine(SpawnObstacles());
        StartCoroutine(SpawnTrains()); // Independent train spawning
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            waveCount++;

            if (waveCount % 5 == 0)
            {
                SpawnThreeObstacles(); // Every 5th wave, spawn 3 obstacles
            }
            else
            {
                SpawnTwoObstacles(); // Otherwise, spawn 2
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnTrains()
    {
        while (true)
        {
            float randomSpawnTime = Random.Range(minTrainSpawnTime, maxTrainSpawnTime); // Randomized train spawn time
            yield return new WaitForSeconds(randomSpawnTime); // Wait before spawning train

            int randomLaneIndex = Random.Range(0, lanes.Length);
            float trainLane = lanes[randomLaneIndex];

            StartCoroutine(SpawnTrain(trainLane)); // Spawn train
            StartCoroutine(BlockLane(trainLane)); // Block lane for 3 seconds
        }
    }

    IEnumerator SpawnTrain(float spawnX)
    {
        float trainSpacing = 1.6f;

        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = new Vector3(spawnX, spawnY + (i * trainSpacing), 0);
            Instantiate(trainPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator BlockLane(float lane)
    {
        blockedLanes.Add(lane);
        yield return new WaitForSeconds(trainLaneBlockTime);
        blockedLanes.Remove(lane);
    }

    private void SpawnTwoObstacles()
    {
        List<float> availableLanes = new List<float>(lanes);
        availableLanes.RemoveAll(lane => blockedLanes.Contains(lane)); 

        if (availableLanes.Count < 2) return;

        int firstLaneIndex = Random.Range(0, availableLanes.Count);
        int secondLaneIndex;
        do
        {
            secondLaneIndex = Random.Range(0, availableLanes.Count);
        } while (secondLaneIndex == firstLaneIndex);

        SpawnObstacle(availableLanes[firstLaneIndex]);
        SpawnObstacle(availableLanes[secondLaneIndex]);
    }

    private void SpawnThreeObstacles()
    {
        List<float> availableLanes = new List<float>(lanes);
        availableLanes.RemoveAll(lane => blockedLanes.Contains(lane)); 

        if (availableLanes.Count < 3) return; 

        foreach (float lane in availableLanes)
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
