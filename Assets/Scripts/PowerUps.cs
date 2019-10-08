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
    public Vector3 yellow1;
    public Vector3 yellow2;

    void Start()
    {
  
    }

    public void Createbox(Vector3 position, Color color)
    {

        var hitColliders = Physics.OverlapSphere(position, 4);
        Debug.Log(hitColliders);
        if (hitColliders.Length  <=2)
        {
            if (color == Color.white)
            {
                Instantiate(whitebox, position, Quaternion.identity);
            }
            if (color == Color.yellow && count_yellow < 2)
            {
                Instantiate(yellowbox, position, Quaternion.identity);
                if (count_yellow == 0)
                {
                    yellow1 = position;
                }
                if (count_yellow == 1)
                {
                    yellow2 = position;
                }
                count_yellow++;
            }
            else {
                //Instantiate(whitebox, position, Quaternion.identity);
            }
            if (color == Color.blue)
            {
                Instantiate(bluebox, position, Quaternion.identity);
            }
            if (color == Color.red)
            {
                Instantiate(redbox, position, Quaternion.identity);
            }
            if (color == Color.green)
            {
                Instantiate(greenbox, position, Quaternion.identity);
            }

        }

    }






}
