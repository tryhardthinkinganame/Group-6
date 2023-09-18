using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Pool;

public class GameController : MonoBehaviour
{
    [Tooltip("A reference to the tile we want to spawn")]
    public Transform tile;

    [Tooltip("A reference to the obstacle we want to spawn")]
    public Transform obstacle;

    [Tooltip("Where the first tile should be placed at")]
    public Vector3 startPoint = new Vector3(0, 0, -5);

    [Tooltip("How many tiles should we create in advance")]
    [Range(1, 15)]
    public int initSpawnNum = 10;

    [Tooltip("How many tiles to spawn initially with no obstacles")]
    public int initNoObstacles = 3;

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
       var newTile = Instantiate(tile, nextTileLocation,nextTileRotation);

        var nextTile = newTile.Find("Next Spawn Point");
        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;

        if (spawnObstacles)
        {
            SpawnObstacle(newTile);
        }

    }

    private void SpawnObstacle(Transform newTile)
    {
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
            int obstaclesToSpawn = Random.Range(1, 3);

            ShuffleList(obstacleSpawnPoints);

            for (int i = 0; i < obstaclesToSpawn; i++)
            {
                var spawnPoint = obstacleSpawnPoints[i];

                var spawnPos = spawnPoint.transform.position;

                var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);

                newObstacle.SetParent(spawnPoint.transform);
            }
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}


