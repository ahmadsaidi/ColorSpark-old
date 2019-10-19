using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineController : MonoBehaviour
{
    public GameObject trap;
    public GameObject door;
    public GameObject bridge;
    public Color color;
    public GameObject boxes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {








    }

   public  void yellow()
    {
        if (color == Color.yellow && door)
        {
            door.SetActive(false);
        }

    }

    public void red()
    {
        if (color == Color.red && bridge)
        {
            bridge.SetActive(true);
        }
    }

    public void blue()
    {
        if (color == Color.blue && boxes)
        {
            Float(boxes);
        }

    }

    public void green()
    {
        if (color == Color.green && trap)
        {


            trap.SetActive(false);
        }
    }

    public void white()
    {
        if (door)
        {
            door.SetActive(true);
        }
        if (bridge)
        {
            bridge.SetActive(false);
        };

        if (trap)
        {
            trap.SetActive(true);
        }
        color = Color.white;
    }
    

    


    void Float(GameObject boxes){

        for (int i = 0; i < boxes.transform.childCount ; i++)
        {
            Debug.Log(boxes.transform);
            boxes.transform.GetChild(i).transform.position  += new Vector3(0, 30, 0);
        }
    }

    public void Fall(GameObject boxes)
    {
        Transform[] ts = boxes.GetComponentsInChildren<Transform>();
        Debug.Log(ts[0]);
        for (int i = 0; i < boxes.transform.childCount; i++)
        {
            boxes.transform.GetChild(i).transform.position -= new Vector3(0, 30, 0);
        }
    }


}


