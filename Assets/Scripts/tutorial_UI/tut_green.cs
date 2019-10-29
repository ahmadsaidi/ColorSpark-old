using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class tut_green : MonoBehaviour
{
    public GameObject player;
    Text txt;
    // keeps track of which step the tut is going at now
    int stage = 0;

    void Start()
    {
        txt = gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        if (stage == 0 && Input.GetAxis("Vertical") != 0)
        {
            txt.text = "Push your left handle left & right to change direction";
            stage = 1;
        }
        if (stage == 1 && Input.GetAxis("Horizontal") != 0)
        {
            txt.text = "Press 'fire1' to Jump!";
            stage = 2;
        }
        if (stage == 2 && Input.GetButtonDown("Fire1"))
        {
            txt.text = "Now walk through the green spark";
            stage = 3;
        }
        if (player.GetComponent<PlayerController>().color == Color.green)
        {
            txt.text = "Walking through spark gives you power up\nGreen spark increases your speed\nTry to hold down 'jump' key to activate speed up";
            stage = 4;
        }
        if (stage == 4 && Input.GetButton("Jump") && player.GetComponent<PlayerController>().color == Color.green && Input.GetAxis("Vertical") != 0)
        {   
            txt.text = "Now rush toward the other side of the trench and JUMP!";
        }
    }
}