using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limo : MonoBehaviour
{
    [SerializeField] private List<Sprite> LimoSprites;
    // Start is called before the first frame update
    void Start()
    {
        int limoNumber = Random.Range(0, LimoSprites.Count);
        this.GetComponent<SpriteRenderer>().sprite = LimoSprites[limoNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
