using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    [SerializeField] private List<Sprite> CarSprites;
    // Start is called before the first frame update
    void Start()
    {
        int carNumber = Random.Range(0, CarSprites.Count);
        this.GetComponent<SpriteRenderer>().sprite = CarSprites[carNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
