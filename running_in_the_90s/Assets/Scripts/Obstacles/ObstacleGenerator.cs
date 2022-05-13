    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private const float DISTANCE_TO_SPAWN = 10f;
    private const int MAX_SPAWNED_PARTS = 10;


    [SerializeField] private Transform ObstaclesStart;
    [SerializeField] private List<Transform> AllObstacles;
    [SerializeField] private Transform Player;

    private float lastEndObstacleX;
    private Transform lastLevelPart;
    private Queue<Transform> spawnedParts;

    // Start is called before the first frame update
    void Start()
    {
        spawnedParts = new Queue<Transform>();
        lastLevelPart = SpawnLevelPart(ObstaclesStart.position);
        lastEndObstacleX = lastLevelPart.Find("EndObstacle").position.x;
        spawnedParts.Enqueue(lastLevelPart);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastEndObstacleX - Player.position.x <= DISTANCE_TO_SPAWN)
        {
            lastLevelPart = SpawnLevelPart(lastLevelPart.Find("EndObstacle").position);
            lastEndObstacleX = lastLevelPart.Find("EndObstacle").position.x;
            spawnedParts.Enqueue(lastLevelPart);
        }
        if(spawnedParts.Count > MAX_SPAWNED_PARTS)
        {
            RemoveLevelPart(spawnedParts.Dequeue());
        }
    }


    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        int obstacleNumber = Random.Range(0, AllObstacles.Count);
        return Instantiate(AllObstacles[obstacleNumber], spawnPosition, Quaternion.identity, this.transform);
    }

    void RemoveLevelPart(Transform part)
    {
        Destroy(part.gameObject);
    }

}
