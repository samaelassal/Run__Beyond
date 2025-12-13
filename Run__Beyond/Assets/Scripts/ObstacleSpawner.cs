using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Data & Lanes")]
    public List<ObstacleData> obstacleTypes; // Assign your SOs here
    public Transform[] spawnPoints;          // Assign Lane1Spawn & Lane2Spawn here

    [Header("Timing (Random Intervals)")]
    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 3.0f;

    private IEnumerator spawnRoutine;

    void Start()
    {
        spawnRoutine = SpawnRoutine();
        StartCoroutine(spawnRoutine);
    }

    // Feature 1: Dynamic Obstacle Spawning
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 1. Random Interval Variation
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            // Check if we should spawn (i.e., not Main Menu or Game Over)
            if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            {
                continue; // Skip the rest of the loop and wait for next interval
            }

            // 2. Randomly pick a lane (2 lanes)
            int laneIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[laneIndex];

            // 3. Randomly pick an obstacle type (variation)
            ObstacleData data = obstacleTypes[Random.Range(0, obstacleTypes.Count)];

            // 4. Instantiate and set speed
            GameObject newObstacle = Instantiate(data.prefab, spawnPoint.position, spawnPoint.rotation);

            ObstacleMovement movement = newObstacle.GetComponent<ObstacleMovement>();
            if (movement != null)
            {
                movement.SetSpeed(data.speedModifier);
            }
        }
    }

    // Helper function to stop/start spawning when pausing/restarting
    public void ToggleSpawning(bool active)
    {
        if (active)
        {
            if (spawnRoutine == null) { spawnRoutine = SpawnRoutine(); }
            StartCoroutine(spawnRoutine);
        }
        else
        {
            if (spawnRoutine != null) { StopCoroutine(spawnRoutine); }
        }
    }
}