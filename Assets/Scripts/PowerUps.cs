using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    bool fast = false;
    float time = 100;
    GameObject[] walls;

     void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("ghost");
    }


    public void ghost(bool on)
    {
       
        foreach(GameObject wall in walls)
        {
            wall.GetComponent<BoxCollider>().enabled = on;
            Color c = wall.GetComponent<Renderer>().material.color;
            c.a = 0.5f;
            if (on)
            {
                c.a = 1.0f;
            }
            
            wall.GetComponent<Renderer>().material.color = c;
        }
    }
}
