using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{

    private float len, startpos;
    public GameObject cam;
    public float parallex_effect;
    // Start is called before the first frame update
    void Start()
    {

        startpos = transform.position.x;
        len = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallex_effect));
        float dist = (cam.transform.position.x * parallex_effect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + len)
            startpos += len;
        else if (temp < startpos - len)
            startpos -= len;

    }
}
