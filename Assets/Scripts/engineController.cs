using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineController : MonoBehaviour
{
    public GameObject trap;
    public GameObject door;
    public GameObject bridge;
    public Color color;
    public GameObject boxes;
    public bool flo = false;
    public bool fall = false;
    int count = 0;

    Animator left;
    Animator right;
    // Start is called before the first frame update
    void Start()
    {
        color = Color.white;

        
    }

    // Update is called once per frame
    void Update()
    {

        if (flo && count < 30)
        {

            for (int i = 0; i < boxes.transform.childCount; i++)
            {
                Debug.Log(boxes.transform);
                Transform box = boxes.transform.GetChild(i);
                float velocity = 60f;
                Vector3 targetposition = box.transform.position + new Vector3(0, 30, 0);
                float newPosition = Mathf.SmoothDamp(box.transform.position.y, targetposition.y, ref velocity, 12f);
                box.transform.position = new Vector3(box.transform.position.x, newPosition, box.transform.position.z);
            }
            count++;
            if (count == 30)
            {
                flo = false;
            }
        }

        if(fall && count > 0)
        {
            Transform[] ts = boxes.GetComponentsInChildren<Transform>();

            for (int i = 0; i < boxes.transform.childCount; i++)
            {
                Debug.Log(boxes.transform);
                Transform box = boxes.transform.GetChild(i);
                float velocity = -60f;
                Vector3 targetposition = box.transform.position - new Vector3(0, 30, 0);
                float newPosition = Mathf.SmoothDamp(box.transform.position.y, targetposition.y, ref velocity, 12f);
                box.transform.position = new Vector3(box.transform.position.x, newPosition, box.transform.position.z);
            }
            count--;
            if (count == 0)
            {
                fall = false;
            }
        }





    }

   public  void yellow()
    {
        if (color == Color.yellow && door)
        {
            //slideDoors(true);
            door.SetActive(false);
        }

    }

    public void red()
    {
        if (color == Color.red && bridge)
        {
            StartCoroutine(buildBridge());
        }

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

    public void blue()
    {
        if (color == Color.blue && boxes)
        {
            fall = false;
            flo = true;
        }

    }

    public void green()
    {
        if (color == Color.green && trap)
        {
           MeshRenderer rend = trap.GetComponent<MeshRenderer>(); ;
           Debug.Log(rend);

           StartCoroutine(FadeOut());

            IEnumerator FadeOut()
            {
                for ( float f = 1f; f >= -0.05f; f-= 0.05f)
                {
                    Color c = rend.material.color;
                    c.a = f;
                    rend.material.color = c;
                    yield return new WaitForSeconds(0.05f);
                }
                trap.SetActive(false);

            }
        
        }
    }

    public void white()
    {
        if (door)
        {
            //slideDoors(false);
            door.SetActive(true);
        }
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

        if (trap)
        {
            MeshRenderer rend = trap.GetComponent<MeshRenderer>(); ;
            Color c = rend.material.color;
            c.a = 0f;
            rend.material.color = c;
            StartCoroutine(FadeIn());

            IEnumerator FadeIn()
            {
                trap.SetActive(true);
                for (float f = 0.05f; f <=  1f; f += 0.05f)
                {
                    Color color = rend.material.color;
                    Debug.Log(color);
                    color.a = f;
                    rend.material.color = color;
                    yield return new WaitForSeconds(0.05f);
                }

            }
        }
        color = Color.white;
    }

    void slideDoors(bool state)
    {
        left.SetBool("slide", state);
        right.SetBool("slide", state);
    }








}


