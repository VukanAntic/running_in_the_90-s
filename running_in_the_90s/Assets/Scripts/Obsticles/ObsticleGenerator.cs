using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleGenerator : MonoBehaviour
{

    [SerializeField] private Transform Obsticles_1;
    // Start is called before the first frame update
    void Start()
    {
        Transform lastLevelPart;
        lastLevelPart = SpawnLevelPart(Obsticles_1.Find("EndObsticle").position);
        lastLevelPart = SpawnLevelPart(lastLevelPart.Find("EndObsticle").position);
        lastLevelPart = SpawnLevelPart(Obsticles_1.Find("EndObsticle").position);
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        return Instantiate(Obsticles_1, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    