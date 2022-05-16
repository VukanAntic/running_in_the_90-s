using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAndLampGenerator : MonoBehaviour
{
    private const float DISTANCE_TO_SPAWN = 10f;
    private const int MAX_SPAWNED_PARTS = 10;

    [SerializeField] private Transform RoadStart;
    [SerializeField] private Transform RoadPart;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform FloorCollider;

    private float lastRoadEndX;
    private Transform lastRoadPart;
    private Queue<Transform> spawnedRoads;
    // Start is called before the first frame update
    void Start()
    {
        spawnedRoads = new Queue<Transform>();
        lastRoadPart = SpawnRoadPart(RoadStart.Find("RoadEnd").position);
        lastRoadEndX = lastRoadPart.Find("RoadEnd").position.x;
        spawnedRoads.Enqueue(lastRoadPart);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastRoadEndX - Player.position.x <= DISTANCE_TO_SPAWN)
        {
            lastRoadPart = SpawnRoadPart(lastRoadPart.Find("RoadEnd").position);
            lastRoadEndX = lastRoadPart.Find("RoadEnd").position.x;
            spawnedRoads.Enqueue(lastRoadPart);
        }
        if (spawnedRoads.Count > MAX_SPAWNED_PARTS)
        {
            RemoveRoadPart(spawnedRoads.Dequeue());
        }
        FloorCollider.position = new Vector2(Player.position.x, FloorCollider.position.y);
    }

    private Transform SpawnRoadPart(Vector3 spawnPosition)
    {
        return Instantiate(RoadPart, spawnPosition, Quaternion.identity, this.transform);
    }

    void RemoveRoadPart(Transform part)
    {
        Destroy(part.gameObject);
    }
}
