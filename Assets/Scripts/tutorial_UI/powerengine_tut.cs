﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerengine_tut : MonoBehaviour
{
    // Start is called before the first frame update
    public Text instruction;
    public int stage;
    public GameObject redEngine;
    public GameObject greenEngine;
    public GameObject blueEngine;
    private GameObject robot;
    public string[] stageInstructions = {"Get Blue Spark," +
                                          "Put spark on engine(Press B)",
                                         "Use Predefined Teleports(RB)",
                                         "Put Green Spark on Engine " + "Build the bridge",
                                         "Get Back to engine1 and Get Spark Again(Press X)" ,
                                          "Put Red Spark on Engine1"};

    private engineController cs0;
    private engineController cs1;
    private engineController cs2;
    PlayerController pc;

    void Start()
    {
        instruction.text = stageInstructions[0];
        blueEngine = GameObject.Find("engine0");
        greenEngine = GameObject.Find("engine1");
        redEngine = GameObject.Find("engine2");
        cs0  = blueEngine.GetComponent<engineController>();
        cs1  = greenEngine.GetComponent<engineController>();
        //cs2  = redEngine.GetComponent<engineController>();
        robot = GameObject.Find("NewModelRobot");
        stage = 0;
        pc = robot.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {




        if (cs0.color == Color.blue && stage < 2) {
            stage = 1;
        }

        if (robot.transform.position.z < ( -150) && stage <3)
        {
            stage = 2;

        }

        if (cs1.color == Color.green && stage < 3)
        {
            stage = 3;

        }

        if (cs0.color == Color.red && stage  == 3)
        {
            stage = 4;

        }
        if (instruction != null)instruction.text = stageInstructions[stage];
    }
}
