using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public GameObject yellowbox;
    public GameObject bluebox;
    public GameObject greenbox;
    public GameObject redbox;
    public GameObject whitebox;
    public GameObject player;
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
            if (color == Color.white && count_white < 7)
            {
                Instantiate(whitebox, position, Quaternion.identity);
                count_white++;
            }
            if (color == Color.yellow && count_yellow < 2)
            {
                
                if (count_yellow == 0)
                {
                    yellowbox1 = Instantiate(yellowbox, position, Quaternion.identity);
     
                }
                if (count_yellow == 1)
                {
                    yellowbox2 = Instantiate(yellowbox, position, Quaternion.identity);
                }
                count_yellow++;
                pc.whitePower();
            }
            
            if (color == Color.blue && count_blue < 3)
            {
                Instantiate(bluebox, position, Quaternion.identity);
                count_blue++;
                pc.whitePower();
            }
            if (color == Color.red && count_red < 2)
            {
                Instantiate(redbox, position, Quaternion.identity);
                count_red++;
                pc.whitePower();
            }
            if (color == Color.green && count_green <4)
            {
                Instantiate(greenbox, position, Quaternion.identity);
                count_green++;
                pc.whitePower();
            }

        }

    }






}
