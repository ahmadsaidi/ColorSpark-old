using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUps : MonoBehaviour
{

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

    public void Createbox(Vector3 position, Color color, int check)
    {
        if (pc.carry)
        {
            return;
        }
        Vector3 original = position;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(5 * forward.z, 8, -5 * forward.x);
        position = position + forward;
        var hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length  <=1 && check == 0)
        {
            

            
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
        else if (check == 0)
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

                    

                    if (color == Color.blue && count_blue < 1)
                    {
                        GameObject spark = Instantiate(bluespark, newpos, Quaternion.identity);
                        spark.GetComponent<SparkController>().eat = false;

                        //  engine_color = Color.yellow;
                        count_blue++;
                        pc.whitePower();
                        gc.color = Color.blue;
                        gc.blue();
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
                    if (color == Color.red && count_red < 1)
                    {
                        GameObject spark = Instantiate(redspark, newpos, Quaternion.identity);
                        spark.transform.parent = hitColliders[i].gameObject.transform;
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
        else if (check == 1)
        {
            hitAndcreate(color, position);
        }

    }
    public void hitAndcreate(Color color, Vector3 position)
    {
        Vector3 original = position;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(5 * forward.z, 0, -5 * forward.x);
        position = position + forward;
        var hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <= 1 && checkTag(hitColliders))
        {
            creatAt(color, position);
            return;

        }
        forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(5 * forward.x, 0, -5 * forward.z);
        position = original + forward;
        hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <=1 && checkTag(hitColliders))
        {
            creatAt(color, position);
            return;

        }
        forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(5 * forward.z, 0, 5 * forward.x);
        position = original + forward;
        hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <= 1 && checkTag(hitColliders))
        {
            creatAt(color, position);
            return;

        }

        forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(5 * forward.x, 0, 5 * forward.z);
        position = original + forward;
        hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <= 1 && checkTag(hitColliders))
        {
            creatAt(color, position);
            return;

        }

        forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(-5 * forward.z, 0, -5 * forward.x);
        position = position + forward;
        hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <= 1 && checkTag(hitColliders))
        {
            creatAt(color, position);
            return;

        }
        forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(-5 * forward.x, 0, -5 * forward.z);
        position = original + forward;
        hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <= 1 && checkTag(hitColliders))
        {
            creatAt(color, position);
            return;

        }
        forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(-5 * forward.z, 0, 5 * forward.x);
        position = original + forward;
        hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <= 1 && checkTag(hitColliders)  )
        {
            creatAt(color, position);
            return;

        }

        forward = transform.TransformDirection(Vector3.forward);
        forward = new Vector3(-5 * forward.x, 0, 5 * forward.z);
        position = original + forward;
        hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <= 1 &&checkTag(hitColliders) )
        {
            creatAt(color, position);
            return;

        }
        forward = transform.TransformDirection(Vector3.left);
        forward = new Vector3(0, 12, 0);
        position = original + forward;
        hitColliders = Physics.OverlapSphere(position, 6);
        if (hitColliders.Length <= 1&& checkTag(hitColliders))
        {



            creatAt(color, position);
            return;

        }

    }

    void creatAt(Color color, Vector3 position)
    {
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
        if (color == Color.green && count_green < 1)
        {
            Instantiate(greenspark, position, Quaternion.identity);
            count_green++;
            pc.whitePower();
        }

    }

    bool checkTag(Collider[] hitColliders)
    {
        for (int i = 0; i < hitColliders.Length; i++)
        {

            if (hitColliders[i].tag == "red "|| hitColliders[i].tag == "blue " || (hitColliders[i].tag == "yellow"))
            {
                return false;
            }
        }
        return true;
    }



    public void Createtele(Vector3 position, Color color)
    {
        if (tele_num == 0)
        {
            yellowbox1 = Instantiate(tele, position , Quaternion.Euler(-90, 0,-180));

        }
        if (tele_num == 1)
        {
            yellowbox2 = Instantiate(tele, position , Quaternion.Euler(-90, 0, -180));
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


                    if (intersecting[j].tag == "green")
                    {
                        intersecting[j].gameObject.SetActive(false);
                        pc.greenPower();
                        gc.white();
                        count_green--;
                        tilePickupAudio.PlayOneShot(mm.greenAudio);
                    }


                    if (intersecting[j].tag == "red")
                    {
                        intersecting[j].gameObject.SetActive(false);
                        pc.redPower();
                        gc.white(); 
                        count_red--;
                        tilePickupAudio.PlayOneShot(mm.redAudio);
                    }


                    if (intersecting[j].tag == "blue")
                    {
                        intersecting[j].gameObject.SetActive(false);
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
