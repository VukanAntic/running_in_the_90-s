using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{

    [SerializeField] private List<Sprite> LampSprites;
    // Start is called before the first frame update
    void Start()
    {
        int lampNumber = Random.Range(0, LampSprites.Count);
        GetComponent<SpriteRenderer>().sprite = LampSprites[lampNumber];
    }
}
