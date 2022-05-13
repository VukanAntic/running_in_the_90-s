using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField] private List<Sprite> UFOSprites;
    private List<Sprite> Chosen;

    private float ColorChangeTime;
    private float MaxColorChangeTime;
    private int CurrentChosen;

    private float Speed;
    private int Direction;
    private float MaxMove;
    private float Move;
    // Start is called before the first frame update
    void Start()
    {
        Chosen = new List<Sprite>();
        int UFONumber = Random.Range(0, UFOSprites.Count);
        Chosen.Add(UFOSprites[UFONumber]);
        Chosen.Add(UFOSprites[(UFONumber+Random.Range(1, UFOSprites.Count))%UFOSprites.Count]);
        this.GetComponent<SpriteRenderer>().sprite = Chosen[0];

        MaxColorChangeTime = Random.Range(3, 7) * 0.1f;
        ColorChangeTime = 0f;
        CurrentChosen = 0;

        Speed = 0.02f * Random.Range(2, 10);
        MaxMove = 0.2f;
        Move = 0f;
        Direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float currMove = Speed * Time.deltaTime;
        transform.position += new Vector3(0, Direction * currMove, 0);
        Move += currMove;
        if (Move >= MaxMove)
        {
            Direction *= -1;
            Move = 0;
        }

        ColorChangeTime += Time.deltaTime;
        if (ColorChangeTime >= MaxColorChangeTime) 
        {
            ColorChangeTime = 0;
            CurrentChosen = (CurrentChosen + 1) % 2;
            this.GetComponent<SpriteRenderer>().sprite = Chosen[CurrentChosen];
        }
    }
}
