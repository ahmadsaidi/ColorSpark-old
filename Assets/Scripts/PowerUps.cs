using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public GameObject yellowspark;
    public GameObject bluespark;
    public GameObject greenspark;
    public GameObject redspark;
    public GameObject player;
    public GameObject tele;
    PlayerController control;
    bool fast, teleport, highJump, push = false;
    public int count_yellow = 0;
    public int count_white = 0;
    public int count_blue = 0;
    public int count_red = 0;
    public int count_green = 0;
    PlayerController pc;
    public GameObject yellowbox1;
    public GameObject yellowbox2;
    public int tele_num = 0;


    void Start()
    {
        pc  = FindObjectOfType<PlayerController>();
    }

    public void Createbox(Vector3 position, Color color)
    {
        
        var hitColliders = Physics.OverlapSphere(position, 4);
        Debug.Log(hitColliders);
        if (hitColliders.Length  <=2)
        {
            
            if (color == Color.yellow && count_yellow <1)
            {

                Instantiate(yellowspark, position, Quaternion.identity);
                count_yellow++;
                pc.whitePower();

            }
            
            if (color == Color.blue && count_blue < 1)
            {
                Instantiate(bluespark, position, Quaternion.identity);
                count_blue++;
                pc.whitePower();
            }
            if (color == Color.red && count_red < 1)
            {
                Instantiate(redspark, position, Quaternion.identity);
                count_red++;
                pc.whitePower();
            }
            if (color == Color.green && count_green <1)
            {
                Instantiate(greenspark, position, Quaternion.identity);
                count_green++;
                pc.whitePower();
            }

        }

    }

    public void Createtele(Vector3 position, Color color)
    {
        if (tele_num == 0)
        {
            yellowbox1 = Instantiate(tele, position, Quaternion.identity);

        }
        if (tele_num == 1)
        {
            yellowbox2 = Instantiate(tele, position, Quaternion.identity);
        }
        tele_num++;

    }
    






    }
