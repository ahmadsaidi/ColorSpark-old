﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class basictut : MonoBehaviour
{
    public Text instruction;
    private PlayerController pc;
    private GameObject robot;
    private GameObject b1;
    private GameObject b2;
    public GameObject blast;
    public GameObject green;
    public GameObject red;
    public GameObject blue;
    private bool redisntpresent = true;
    private bool greenisntpresent = true;
    GameManager gm;

    private string[] instructions = {
        "Move forward (Left Handle)",
        "Change your direction (Left Handle)",
        "Jump to pass obstacles (A)",
        "Pick up blue spark (walk through)",
        "Pick up red spark (walk through)",
        "Pick up green spark (walk through)",
        "Carry the Box (X)",
        "Pass the portal(RB) & Play around\n Press Y for next level"
    };
    private int stage = 0; 
    

    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find("NewModelRobot");
        pc = robot.GetComponent<PlayerController>();
        gm = FindObjectOfType<GameManager>();
        green.SetActive(false);
        red.SetActive(false);
        

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Vertical") != 0)
        {
            instructions[0] = null;
        }

        if (Input.GetAxis("Horizontal") != 0){
            instructions[1] = null;
        }

        if (Input.GetButton("Fire1")){
            instructions[2] = null;
        }

        if (instructions [3] == null && instructions [4] == null && instructions [5] == null){
            stage = 1;
        }

        for (int i = 0; i < instructions.Length; i++){
            if (i == instructions.Length-1){
                stage = 2;
 
            }
            if (instructions[i]!= null){
                instruction.text = instructions[i];
                break;
            }
            
        }

        if (blast == null){
            instructions[4] = null;
           
        }

        if(instructions[3] == null && redisntpresent){
            red.SetActive(true);
            blue.SetActive(false);
            redisntpresent = false;
        }
        if(instructions[4] == null && greenisntpresent){
            green.SetActive(true);
            red.SetActive(false);
            blue.SetActive(false);
            greenisntpresent = false;
        }

        if (pc.color == Color.blue && instructions[3]!=null){
            b1 = robot.GetComponent<PowerUps>().yellowbox1;
            b2 = robot.GetComponent<PowerUps>().yellowbox2;
            if (b1 != null && b2 != null){
                instructions[3] = null;
            }
            else{
                instruction.text = "Use blue to create portals (RB)";
            }
        }
        // detect pass portals
        if (pc.color ==  Color.red && instructions[4]!= null){
            instruction.text = "Use Red to destroy the red materials (RB)";
        }
        // detect wall destroyed 
        if (pc.color ==  Color.green && instructions[5]!= null){
            instruction.text = "Use green to speed up (hold RB)";
        }
        if (Input.GetButton("Jump") && pc.color == Color.green && Input.GetAxis("Vertical") != 0){
            instructions[5] = null;
        }
        
        if ((stage == 0) && ((pc.color == Color.red && instructions[4]== null) || (pc.color ==  Color.green && instructions[5]== null) || (pc.color ==  Color.blue && instructions[3]== null))){
            instruction.text = "Drop(Press B) current spark ";
            Debug.Log(stage);
        }
        if (pc.carry){
            instructions[6] = null;
            instruction.text = "move around & drop the box (X)";
        }
        
        if (stage == 2 && Input.GetButtonDown("Carry")){
            gm.Intro2();
        }
    }
}
