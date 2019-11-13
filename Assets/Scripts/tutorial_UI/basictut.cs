using System.Collections;
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
    GameManager gm;

    private string[] instructions = {
        "Move forward (Left Handle)",
        "Change your direction (Left Handle)",
        "Jump (A)",
        "Carry the Box (X)",
        "Pick up blue spark (walk through)",
        "Pick up red spark (walk through)",
        "Pick up green spark (walk through)",
        "Pass the portal(RB) & Play around\n Press Y for next level"
    };
    private int stage = 0; 
    

    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find("NewModelRobot");
        pc = robot.GetComponent<PlayerController>();
        gm = FindObjectOfType<GameManager>();

        
        
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

        if (instructions [4] == null && instructions [5] == null && instructions [6] == null){
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
            instructions[5] = null;
        }

        if (pc.color == Color.blue && instructions[4]!=null){
            b1 = robot.GetComponent<PowerUps>().yellowbox1;
            b2 = robot.GetComponent<PowerUps>().yellowbox2;
            if (b1 != null && b2 != null){
                instructions[4] = null;
            }
            else{
                instruction.text = "Use blue to create portals (RB)";
            }
        }
        // detect pass portals
        if (pc.color ==  Color.red && instructions[5]!= null){
            instruction.text = "Use Red to destroy the red materials (RB)";
        }
        // detect wall destroyed 
        if (pc.color ==  Color.green && instructions[6]!= null){
            instruction.text = "Use green to speed up (hold RB)";
        }
        if (Input.GetButton("Jump") && pc.color == Color.green && Input.GetAxis("Vertical") != 0){
            instructions[6] = null;
        }
        
        if ((stage == 0) && (pc.color == Color.red && instructions[5]== null) || (pc.color ==  Color.green && instructions[6]== null) || (pc.color ==  Color.blue && instructions[4]== null)){
            instruction.text = "Drop spark (B) to take another";
        }
        if (pc.carry){
            instructions[3] = null;
            instruction.text = "move around & drop the box (X)";
        }
        
        if (stage == 2 && Input.GetButtonDown("Carry")){
            Debug.Log("NEXT");
            gm.WinLevel();
        }
    }
}
