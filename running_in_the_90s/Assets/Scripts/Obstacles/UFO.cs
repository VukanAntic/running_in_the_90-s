using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField] private List<Sprite> UFOSprites;
    // Start is called before the first frame update
    void Start()
    {
        int UFONumber = Random.Range(0, UFOSprites.Count);
        this.GetComponent<SpriteRenderer>().sprite = UFOSprites[UFONumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
