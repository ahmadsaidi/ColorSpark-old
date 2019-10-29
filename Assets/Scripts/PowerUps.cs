using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUps : MonoBehaviour
{

    public GameObject yellowspark;
    public GameObject bluespark;
    public GameObject greenspark;
    public GameObject redspark;
    public GameObject player;
    public GameObject tele;
    PlayerController control;
    public AudioSource tilePickupAudio;
    bool fast, teleport, highJump, push = false;
    public int count_yellow = 0;
    public int count_white = 0;
    public int count_blue = 0;
    public int count_red = 0;
    public int count_green = 0;
    PlayerController pc;
    public GameObject yellowbox1;
    public GameObject yellowbox2;
    public int tele_num = 0;
    MusicManager mm;
   // public Color engine_color;


    void Start()
    {
        pc  = FindObjectOfType<PlayerController>();
        mm = FindObjectOfType<MusicManager>();
        tilePickupAudio = mm.GetComponent<AudioSource>();


    }

    public void Createbox(Vector3 position, Color color)
    {
        
        var hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length  <=1)
        {
            
            if (color == Color.yellow && count_yellow <1)
            {

                Instantiate(yellowspark, position, Quaternion.identity);
                count_yellow++;
                pc.whitePower();

            }
            
            if (color == Color.blue && count_blue < 1)
            {
                Instantiate(bluespark, position, Quaternion.identity);
                count_blue++;
                pc.whitePower();
            }
            if (color == Color.red && count_red < 1)
            {
                Instantiate(redspark, position, Quaternion.identity);
                count_red++;
                pc.whitePower();
            }
            if (color == Color.green && count_green <1)
            {
                Instantiate(greenspark, position, Quaternion.identity);
                count_green++;
                pc.whitePower();
            }

        }
        else
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i].tag == "engine")
                {
                    Vector3 newpos = hitColliders[i].transform.position + new Vector3(0, 10, 0);
                    engineController gc = hitColliders[i].GetComponent<engineController>();
                    if (gc.color != Color.white)
                    {
                        return;
                    }
                    if (color == Color.yellow && count_yellow < 1)
                    {

                        GameObject spark = Instantiate(yellowspark, newpos, Quaternion.identity);
                        spark.GetComponent<SparkController>().eat = false;
                        
                        //  engine_color = Color.yellow;
                        count_yellow++;                 
                        pc.whitePower();
                        gc.color = Color.yellow;
                        gc.yellow();
                        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("tele");
                        for (var index = 0; index < gameObjects.Length; index++)
                        {
                            Destroy(gameObjects[index]);
                        }
                        yellowbox1 = null;
                        yellowbox2 = null;
                        tele_num = 0;
                        tilePickupAudio.PlayOneShot(mm.spark_to_engine);

                    }

                    if (color == Color.blue && count_blue < 1)
                    {
                        GameObject spark = Instantiate(bluespark, newpos, Quaternion.identity);
                        spark.GetComponent<SparkController>().eat = false;
                        count_blue++;
                        //  engine_color = Color.blue;
                        pc.whitePower();
                        gc.color = Color.blue;
                        gc.blue();
                        tilePickupAudio.PlayOneShot(mm.spark_to_engine);

                    }
                    if (color == Color.red && count_red < 1)
                    {
                        GameObject spark = Instantiate(redspark, newpos, Quaternion.identity);
                        spark.GetComponent<SparkController>().eat = false;
                        count_red++;
                        //engine_color = Color.red;
                        pc.whitePower();
                        gc.color = Color.red;
                        gc.red();
                        tilePickupAudio.PlayOneShot(mm.spark_to_engine);

                    }
                    if (color == Color.green && count_green < 1)
                    {
                        GameObject spark = Instantiate(greenspark, newpos, Quaternion.identity);
                        spark.GetComponent<SparkController>().eat = false;
                        count_green++;
                        //engine_color = Color.green;
                        pc.whitePower();
                        gc.color = Color.green;
                        gc.green();
                        tilePickupAudio.PlayOneShot(mm.spark_to_engine);
                    }

                   
                }

                // scripts for TUT1 
                if (hitColliders[i].name == "mission")
                {
                    Vector3 newpos = hitColliders[i].transform.position + new Vector3(0, 10, 0);
                    if (color == Color.blue){
                        GameManager gm = FindObjectOfType<GameManager>();
                        gm.WinLevel();
                    }
                }
            }
        }

    }

    public void Createtele(Vector3 position, Color color)
    {
        if (tele_num == 0)
        {
            yellowbox1 = Instantiate(tele, position, Quaternion.identity);

        }
        if (tele_num == 1)
        {
            yellowbox2 = Instantiate(tele, position, Quaternion.identity);
            pc.whitePower();
        }
        tele_num++;

    }


    public void GetEnginePower(Vector3 position)
    {
        var hitColliders = Physics.OverlapSphere(position, 6);
        Vector3 newpos;
        Collider[] intersecting;

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "engine" && pc.color == Color.white)
            {
                newpos = hitColliders[i].transform.position + new Vector3(0, 10, 0);
                intersecting = Physics.OverlapSphere(newpos, 3);
                engineController gc = hitColliders[i].GetComponent<engineController>();


                if (intersecting.Length < 1)
                {
                    return;
                }

                for (int j = 0; j < intersecting.Length; j++)
                {
                    if (intersecting[j].tag == "yellow")
                    {
                        intersecting[j].gameObject.SetActive(false);
                        pc.yellowPower();
                        gc.white();
                        count_yellow--;
                        tilePickupAudio.PlayOneShot(mm.yellowAudio);

                    }

                    if (intersecting[j].tag == "green")
                    {
                        intersecting[0].gameObject.SetActive(false);
                        pc.greenPower();
                        gc.white();
                        count_green--;
                        tilePickupAudio.PlayOneShot(mm.greenAudio);
                    }


                    if (intersecting[j].tag == "red")
                    {
                        intersecting[0].gameObject.SetActive(false);
                        pc.redPower();
                        gc.white(); 
                        count_red--;
                        tilePickupAudio.PlayOneShot(mm.redAudio);
                    }


                    if (intersecting[j].tag == "blue")
                    {
                        intersecting[0].gameObject.SetActive(false);
                        pc.bluePower();
                        GameObject Boxes = gc.objectToFloat;
                        gc.white();
                        gc.flo = false;
                        gc.fall = true;
                        tilePickupAudio.PlayOneShot(mm.blueAudio);

                        count_blue--;
                    }




                }

            }
        }
        


                
    }








}
