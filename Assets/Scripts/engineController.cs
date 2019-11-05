using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool flo = false;
    public bool fall = false;
    int count = 0;

    Animator left;
    Animator right;


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

            Debug.Log(objectToFloat.transform.GetChild(0).transform.position);
            Debug.Log(ending);
            if (objectToFloat.transform.GetChild(0).transform.position.y > ending.y)
            {
                flo = false;
                fall = true;

            }
            //count++;
            ////if (count == 200)
            ////{
            ////    flo = false;
            ////    fall = true;
            ////}

            //////if (count == 2000)
            //////{
            //////    flo = false;
            //////    fall = true;
            //////}
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
            //count--;
            //if (count == 0)
            //{
            //    fall = false;
            //    flo = true;
            //}
        }





    }


    

    public void red()
    {

        if (color == Color.red && objectToFloat)
        {
            fall = false;
            flo = true;
            trigger = true;
        }
    }


    public void blue()
    {
        if (color == Color.blue)
        {
            //slideDoors(true);
            yellowbox1.SetActive(true);
            yellowbox2.SetActive(true);


        }

    }

    public void green()
    {
        if (color == Color.green && bridge)
        {
           
            StartCoroutine(buildBridge());
            

            IEnumerator buildBridge()
            {
                for (int i = 0; i < bridge.transform.childCount; i++)
                {
                    GameObject piece = bridge.transform.GetChild(i).gameObject;
                    piece.SetActive(true);
                    yield return new WaitForSeconds(0.1f);

                }

            }
        }
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
        };


        

        
        color = Color.white;
    }

    void slideDoors(bool state)
    {
        left.SetBool("slide", state);
        right.SetBool("slide", state);
    }








}


