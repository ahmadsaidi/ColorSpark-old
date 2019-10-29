using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject floor;
    public GameObject eastwall;
    public GameObject westwall;
    public GameObject southwall;
    public GameObject northwall;
    public float width;
    public float height;
    public float xoffset;
    public float zoffset;

   // public GameObject eastsouthCorner;
   // public GameObject westsouthCorner;



    void Start()
    {
        FloorGenerator();
        EastGenerator();
        WestGenerator();
        SouthGenerator();
        NorthGenerator();
    }

    void FloorGenerator()
    {

        for (int x = 0; x < width *2; x++)
        {
            for (int z = 0; z < height *2; z++)
            {
                GameObject temp = Instantiate(floor);
                temp.transform.position = new Vector3(xoffset +  x * 50, 0,zoffset + z * 50);
            }
        }
    }

     void WestGenerator()
    {

       // GameObject corner = Instantiate(westsouthCorner);
     ///  corner.transform.position = new Vector3(xoffset + 15, 61, -zoffset );

        for (int x = 0; x < height; x++)
        {
            GameObject temp = Instantiate(westwall);
            temp.transform.position = new Vector3(xoffset + x * 40 ,0, -zoffset + 2);
            temp.transform.position = new Vector3(xoffset + x * 40, 0, -zoffset + 2);
        }

    }

    void EastGenerator()
    {


       // GameObject corner = Instantiate(eastsouthCorner);
        //corner.transform.position = new Vector3(xoffset + 15, 61, zoffset);

        for (int x = 0; x < height; x++)
        {
            GameObject temp = Instantiate(eastwall);
            temp.transform.position = new Vector3(xoffset + x * 40, 0, zoffset  - 5);
        }

    }

    void SouthGenerator()
    {
        for (int x = 0; x < width; x++)
        {
            GameObject temp = Instantiate(southwall);
            temp.transform.position = new Vector3(xoffset +10 , 0, zoffset + x * 40);
        }

    }

    void NorthGenerator()
    {
        for (int x = 0; x < width; x++)
        {
            GameObject temp = Instantiate(northwall);
            temp.transform.position = new Vector3(-xoffset  - 25, 0, zoffset + x * 40 + 40);
        }

    }

    void CornerGenerator()
    {
        

    }



}
