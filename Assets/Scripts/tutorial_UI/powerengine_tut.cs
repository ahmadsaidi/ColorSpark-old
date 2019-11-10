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
    public GameObject greenEngine;
    public GameObject blueEngine;
    private GameObject robot;
    public string[] stageInstructions = {"Activate the portal",
                                         "Build the bridge",
                                         "Turn on the elevator" };

    private engineController cs0;
    private engineController cs1;
    private engineController cs2;

    void Start()
    {
        instruction.text = stageInstructions[0];
        blueEngine = GameObject.Find("engine0");
        greenEngine = GameObject.Find("engine1");
        redEngine = GameObject.Find("engine2");
        cs0  = blueEngine.GetComponent<engineController>();
        cs1  = greenEngine.GetComponent<engineController>();
        cs2  = redEngine.GetComponent<engineController>();
        robot = GameObject.Find("Robot+LED");
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {   

        if (cs0.color != Color.white && stage != 1) {
            stage = 1;
        }

        if (cs1.color != Color.white && stage != 2) {
            stage = 2;
            
        }
        if (instruction != null)instruction.text = stageInstructions[stage];
    }
}
