using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{
    [Tooltip("A reference to the tile we want to spawn")]
    public Transform tile;
    [Tooltip("A reference to the obstacle prefabs")]
    public Transform[] obstaclePrefabs;
    [Tooltip("Where the first tile should be placed at")]
    public Vector3 startPoint = new Vector3(0, 0, -5);
    [Tooltip("How many tiles should we create in advance")]
    [Range(1, 15)]
    public int initSpawnNum = 10;
    [Tooltip("How many tiles to spawn initially with no obstacles")]
    public int initNoObstacles = 4;

    private Vector3 nextTileLocation;
    private Quaternion nextTileRotation;

    private void Start()
    {
        nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;
        for (int i = 0; i < initSpawnNum; ++i)
        {
            SpawnNextTile(i >= initNoObstacles);
        }
    }

    public void SpawnNextTile(bool spawnObstacles = true)
    {
        var newTile = Instantiate(tile, nextTileLocation, nextTileRotation);
        var nextTile = newTile.Find("Spawn Point");
        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;
        if (spawnObstacles)
        {
            SpawnRandomObstacle(newTile);
        }
    }

    private void SpawnRandomObstacle(Transform newTile)
    {
        if (obstaclePrefabs.Length == 0)
        {
            Debug.LogWarning("No obstacle prefabs assigned.");
            return;
        }

        var obstacleSpawnPoints = new List<GameObject>();

        foreach (Transform child in newTile)
        {
            if (child.CompareTag("ObstacleSpawn"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }

        if (obstacleSpawnPoints.Count > 0)
        {
            var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];
            var spawnPos = spawnPoint.transform.position;

            // Randomly choose an obstacle prefab
            var randomObstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            var newObstacle = Instantiate(randomObstaclePrefab, spawnPos, Quaternion.identity);
            newObstacle.SetParent(spawnPoint.transform);
        }
    }
}

