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

    [SerializeField] private List<Sprite> CarSprites;
    [SerializeField] private List<Sprite> DroneSprites;
    [SerializeField] private List<Sprite> LimoSprites;
    [SerializeField] private List<Sprite> UFOSprites;

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
        Transform obstacle = Instantiate(AllObstacles[obstacleNumber], spawnPosition, Quaternion.identity, this.transform);
        switch(obstacleNumber)
        {
            case 0:
                int carNumber = Random.Range(0, CarSprites.Count);
                obstacle.Find("Car").GetComponent<SpriteRenderer>().sprite = CarSprites[carNumber];
                break;
            case 1:
                int droneNumber = Random.Range(0, DroneSprites.Count);
                obstacle.Find("Drone_1").GetComponent<SpriteRenderer>().sprite = DroneSprites[droneNumber];
                droneNumber = Random.Range(0, DroneSprites.Count);
                obstacle.Find("Drone_2").GetComponent<SpriteRenderer>().sprite = DroneSprites[droneNumber];
                break;
            case 2:
                int limoNumber = Random.Range(0, LimoSprites.Count);
                obstacle.Find("Limo").GetComponent<SpriteRenderer>().sprite = LimoSprites[limoNumber];
                break;
            case 3:
                int UFONumber = Random.Range(0, UFOSprites.Count);
                obstacle.Find("UFO_1").GetComponent<SpriteRenderer>().sprite = UFOSprites[UFONumber];
                UFONumber = Random.Range(0, UFOSprites.Count);
                obstacle.Find("UFO_2").GetComponent<SpriteRenderer>().sprite = UFOSprites[UFONumber];
                break;
            default:
                break;
        }
        return obstacle;
    }

    void RemoveLevelPart(Transform part)
    {
        Destroy(part.gameObject);
    }

}
