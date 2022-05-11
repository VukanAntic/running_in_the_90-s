using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleGenerator : MonoBehaviour
{
    private const float DISTANCE_TO_SPAWN = 10f;
    private const int MAX_SPAWNED_PARTS = 10;


    [SerializeField] private Transform ObsticlesStart;
    [SerializeField] private List<Transform> AllObsticles;
    [SerializeField] private Transform Player;

    [SerializeField] private List<Sprite> CarSprites;
    [SerializeField] private List<Sprite> LimoSprites;

    private float lastEndObsticleX;
    private Transform lastLevelPart;
    private Queue<Transform> spawnedParts;

    // Start is called before the first frame update
    void Start()
    {
        spawnedParts = new Queue<Transform>();
        lastLevelPart = SpawnLevelPart(ObsticlesStart.position);
        lastEndObsticleX = lastLevelPart.Find("EndObsticle").position.x;
        spawnedParts.Enqueue(lastLevelPart);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastEndObsticleX - Player.position.x <= DISTANCE_TO_SPAWN)
        {
            lastLevelPart = SpawnLevelPart(lastLevelPart.Find("EndObsticle").position);
            lastEndObsticleX = lastLevelPart.Find("EndObsticle").position.x;
            spawnedParts.Enqueue(lastLevelPart);
        }
        if(spawnedParts.Count > MAX_SPAWNED_PARTS)
        {
            RemoveLevelPart(spawnedParts.Dequeue());
        }
    }


    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        int obsticleNumber = Random.Range(0, AllObsticles.Count);
        Transform obsticle = Instantiate(AllObsticles[obsticleNumber], spawnPosition, Quaternion.identity, this.transform);
        switch(obsticleNumber)
        {
            case 0:
                int carNumber = Random.Range(0, CarSprites.Count);
                obsticle.Find("Car").GetComponent<SpriteRenderer>().sprite = CarSprites[carNumber];
                break;
            case 2:
                int limoNumber = Random.Range(0, LimoSprites.Count);
                obsticle.Find("Limo").GetComponent<SpriteRenderer>().sprite = LimoSprites[limoNumber];
                break;
            default:
                break;
        }
        return obsticle;
    }

    void RemoveLevelPart(Transform part)
    {
        Destroy(part.gameObject);
    }

}
