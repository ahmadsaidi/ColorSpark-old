using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerengine_tut : MonoBehaviour
{
    // Start is called before the first frame update
    public Text instruction;
    public int stage;
    public GameObject redEngine;
    public GameObject yellowEngine;
    public GameObject greenEngine;
    public GameObject blueEngine;
    private GameObject robot;
    public string[] stageInstructions = {"Put the red power on the power engine to destroy the wall.",
                                         "Put the yellow power on the power engine to use pre-defined portals.",
                                         "Press RB in front of pre-defiend portal to use it" ,
                                         "Put the green power on the power engine to build a brige.",
                                         "Step on the box right beside the power engine and put the blue power on the power engine to make the box float."};

    private engineController cs0;
    private engineController cs1;
    private engineController cs2;
    private engineController cs3;

    void Start()
    {
        instruction = GetComponent<Text>();
        redEngine = GameObject.Find("engine0");
        yellowEngine = GameObject.Find("engine1");
        greenEngine = GameObject.Find("engine2");
        blueEngine = GameObject.Find("engine3");
        cs0  = redEngine.GetComponent<engineController>();
        cs1  = yellowEngine.GetComponent<engineController>();
        cs2  = greenEngine.GetComponent<engineController>();
        cs3  = blueEngine.GetComponent<engineController>();
        robot = GameObject.Find("Robot_UV");
    stage = 0;
    }

    // Update is called once per frame
    void Update()
    {   

        if (cs0.color != Color.white && stage != 1) {
            stage = 1;
            instruction.color = Color.yellow;
        };



        if (cs1.color != Color.white && stage != 2) {
            stage = 2;
        }

        Debug.Log(robot.transform.position.y);
        if (robot.transform.position.y > 30 && stage < 3)
        {
            stage = 3;
            instruction.color = Color.green;
        }

        if (cs2.color != Color.white && stage != 4) {
            stage = 4;
            instruction.color = Color.blue;
        }
        if (instruction != null && instruction.text != null){
            instruction.text = stageInstructions[stage];
        }
    }
}
