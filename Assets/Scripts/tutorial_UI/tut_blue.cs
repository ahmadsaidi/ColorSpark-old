using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class tut_blue : MonoBehaviour
{
    public GameObject player;
    Text txt;
    // keeps track of which step the tut is going at now
    int stage = 0;

    void Start()
    {
        txt = gameObject.GetComponent<Text>();
        // player.GetComponent<PlayerController>().greenPower();
    }
    private void Update()
    {
        
        if (player.GetComponent<PlayerController>().color == Color.green)
        {
            txt.text = "Walking through spark gives you power up\nGreen park increases your speed\nTry to hold down 'jump' key to activate speed up";
            stage = 4;
        }
        if (stage == 4 && Input.GetButton("Jump") && player.GetComponent<PlayerController>().color == Color.green && Input.GetAxis("Vertical") != 0)
        {   
            txt.text = "Now rush toward the other side of the trench and JUMP!";
        }
    }
}