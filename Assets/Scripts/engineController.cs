﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class engineController : MonoBehaviour
{

    public GameObject yellowbox1;
    public GameObject yellowbox2;
    public GameObject bridge;
    public GameObject objectToFloat;
    public int floatHeight;
    public Color color;
    Vector3 begining ;
    Vector3 ending;
    public bool trigger = false;

    PowerUps pu;
    public bool flo = true;
    public bool fall = false;
    int count = 0;
    public EngineIcon Icon;
    Animator left;
    Animator right;
    GameObject main;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        color = Color.white;
        pu = FindObjectOfType<PowerUps>();
        if (objectToFloat)
        {


                Transform box = objectToFloat.transform.GetChild(0);
                begining = (box.transform.position);
                Vector3 targetposition = box.transform.position + new Vector3(0, floatHeight, 0);
                ending = targetposition;

   
  




        }
        main = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        player = GameObject.FindGameObjectsWithTag("Player")[0];


    }
    // Update is called once per frame
    void Update()
    {
        if (flo && trigger)
        //if (flo && count < 2000 && trigger)
        {


            for (int i = 0; i < objectToFloat.transform.childCount; i++)
            {
                Transform box = objectToFloat.transform.GetChild(i);
                float velocity = 6f;
                Vector3 targetposition = box.transform.position + new Vector3(0, 1, 0);
                float newPosition = Mathf.SmoothDamp(box.transform.position.y, targetposition.y, ref velocity, 6f);
                box.transform.position = new Vector3(box.transform.position.x, newPosition, box.transform.position.z);
            }

            //Debug.Log(objectToFloat.transform.GetChild(0).transform.position);
            //Debug.Log(ending);
            if (objectToFloat.transform.GetChild(0).transform.position.y > ending.y)
            {
                flo = false;
                fall = true;

            }

        }

        if(fall && trigger)
        {
            Transform[] ts = objectToFloat.GetComponentsInChildren<Transform>();

            for (int i = 0; i < objectToFloat.transform.childCount; i++)
            {
                Debug.Log(objectToFloat.transform);
                Transform box = objectToFloat.transform.GetChild(i);
                float velocity = -6f;
                Vector3 targetposition = box.transform.position - new Vector3(0, 1, 0);
                float newPosition = Mathf.SmoothDamp(box.transform.position.y, targetposition.y, ref velocity, 6f);
                box.transform.position = new Vector3(box.transform.position.x, newPosition, box.transform.position.z);
            }

            if (objectToFloat.transform.GetChild(0).transform.position.y < begining.y)
            {
                fall = false;
                flo = true;

            }

        }





    }


    

    public void red()
    {
        if (color == Color.red && objectToFloat)
        {
            if (objectToFloat.transform.childCount <= 0)
            {
                return;
            }
            StartCoroutine(startFloat());
            IEnumerator startFloat()
            {

                //player.GetComponent<PlayerController>().enabled = false;

                //main.GetComponent<cameraCollision>().focus = true;
                //Quaternion torobot = main.transform.rotation;

                Transform box = objectToFloat.transform.GetChild(0);
                //main.transform.position = box.position + new Vector3(20, 3, 0);
                //main.transform.LookAt(box.position);
                if (color == Color.red && objectToFloat)
                {
                    //fall = false;
                    //flo = true;
                    trigger = true;
                }
                yield return new WaitForSeconds(3f);

                //main.GetComponent<cameraCollision>().focus = false;
                //main.transform.rotation = torobot;
                //player.GetComponent<PlayerController>().enabled = true;
            }

            Icon.GetComponent<Image>().color = Color.white;
            Icon.GetComponent<Image>().sprite = Icon.Float;
        }



    }


    public void blue()
    {
        if (color == Color.blue)
        {
            //slideDoors(true);
            StartCoroutine(buildTele());
            IEnumerator buildTele()
            {
                player.GetComponent<PlayerController>().enabled = false;

                main.GetComponent<cameraCollision>().focus = true;
                Quaternion torobot = main.transform.rotation;

                main.transform.position = yellowbox1.transform.position + new Vector3(15, 0, -5);
                main.transform.LookAt(yellowbox1.transform.position);
                yellowbox1.SetActive(true);    
                yield return new WaitForSeconds(1.5f);
 
                main.transform.position = yellowbox2.transform.position + new Vector3(15,0,-5);
                main.transform.LookAt(yellowbox2.transform.position);
                yellowbox2.SetActive(true);
                yield return new WaitForSeconds(1.5f);

                main.GetComponent<cameraCollision>().focus = false;
                main.transform.rotation = torobot;
                player.GetComponent<PlayerController>().enabled = true;

            }



        }
        Icon.GetComponent<Image>().color = Color.white;
        Icon.GetComponent<Image>().sprite = Icon.Teleport;

    }

    public void green()
    {
        if (color == Color.green && bridge)
        {

            StartCoroutine(buildBridge());

            IEnumerator buildBridge()
            {
                player.GetComponent<PlayerController>().enabled = false;

                main.GetComponent<cameraCollision>().focus = true;
                Quaternion torobot = main.transform.rotation;

                main.transform.position = bridge.transform.position + new Vector3(10, 2, 0);
                main.transform.LookAt(bridge.transform.position);
                for (int i = 0; i < bridge.transform.childCount; i++)
                {

                    GameObject piece = bridge.transform.GetChild(i).gameObject;
                    piece.SetActive(true);
                    yield return new WaitForSeconds(1f);

                }
                main.GetComponent<cameraCollision>().focus = false;
                main.transform.rotation = torobot;
                player.GetComponent<PlayerController>().enabled = true;

            }



        }
        Icon.GetComponent<Image>().color = Color.white;
        Icon.GetComponent<Image>().sprite = Icon.plateform;

    }

    public void white()
    {
        if (yellowbox1 || yellowbox2)
        {
            yellowbox1.SetActive(false);
            yellowbox2.SetActive(false);
        }

        trigger = false;

        if (bridge)
        {
            StartCoroutine(collapseBridge());

            IEnumerator collapseBridge()
            {
                for (int i = 0; i < bridge.transform.childCount; i++)
                {
                    GameObject piece = bridge.transform.GetChild(i).gameObject;
                    piece.SetActive(false);
                    yield return new WaitForSeconds(0.1f);

                }

            }

            Icon.GetComponent<Image>().color = Color.black;
            Icon.GetComponent<Image>().sprite = null;
        };


        

        
        color = Color.white;
    }

    void slideDoors(bool state)
    {
        left.SetBool("slide", state);
        right.SetBool("slide", state);
    }








}


