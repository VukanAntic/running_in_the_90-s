using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            startMenu.SetActive(false);
        }
    }

  
}
