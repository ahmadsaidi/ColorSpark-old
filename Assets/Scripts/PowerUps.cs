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

    void Start()
    {
  
    }

    public void Createbox(Vector3 position, Color color)
    {

        var hitColliders = Physics.OverlapSphere(position + new Vector3(5, 4, 5), 7);
        Debug.Log(hitColliders.Length);
        if (hitColliders.Length  <= 2)
        {
            if (color == Color.white)
            {
                Instantiate(whitebox, position + new Vector3(5, 3, 5), Quaternion.identity);
            }
            if (color == Color.yellow)
            {
                Instantiate(yellowbox, position + new Vector3(5, 3, 5), Quaternion.identity);
            }
            if (color == Color.blue)
            {
                Instantiate(bluebox, position + new Vector3(5, 3, 5), Quaternion.identity);
            }
            if (color == Color.red)
            {
                Instantiate(redbox, position + new Vector3(5, 3, 5), Quaternion.identity);
            }
            if (color == Color.green)
            {
                Instantiate(greenbox, position + new Vector3(5, 3, 5), Quaternion.identity);
            }

        }

    }






}
