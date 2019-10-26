using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;

    public float speed;
    public float rotationSpeed;
    public AudioSource goodpick;
    public float jumpspeed = 7000;
    public Color color = Color.white;
    PowerUps powerups;
    public AudioClip blueAduio, greenAudio, redAudio, yellowAudio;
    public AudioClip pushboxAudio, runfasterAudio, teleportAudio, jumphigherAudio;
    public AudioSource tilePickupAudio;
    bool eat = false;
    bool jump = true;
    bool teleport = false;
    GameManager gm;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 40;
        rotationSpeed = 100;
        rb.freezeRotation = true;
        powerups = rb.gameObject.GetComponent<PowerUps>();
        gm = FindObjectOfType<GameManager>();
    }


    private void Update()
    {
        if (transform.position.y < -100)
        {
            gm.LoseGame();

        }
        float translationx = Input.GetAxis("Vertical") * speed;
        float translationz = Input.GetAxis("Horizontal") * speed;
        float rotation = Input.GetAxis("Horizontal camera") * rotationSpeed;
        translationx *= Time.deltaTime;
        translationz *= Time.deltaTime;
        rotation *= Time.deltaTime;


        transform.Translate(translationx,0, -translationz );
        transform.Rotate( 0,rotation, 0);

        if (Input.GetButtonDown("Fire1") && jump == true)
        {
            // red for high jump
            //if (color == Color.red) {
            //   rb.AddForce(Vector3.up * 1.3f * jumpspeed);
            //}
            //else
            //{
               rb.AddForce(Vector3.up * jumpspeed);
            //}
            jump = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            // get forward direciton
            Vector3 forward = transform.TransformDirection (Vector3.forward);
            forward = new Vector3(5*forward.z, 8, -5*forward.x);
            powerups.Createbox(transform.position + forward, color);
            

        }

        if (Input.GetButtonDown("Fire3"))
        {
            powerups.GetEnginePower(transform.position);
        }

        if (Input.GetKey("r"))
        {
            gm.RestartLevel();
        }

        if (Input.GetButtonDown("Jump") && (color == Color.red) )
        {
            var hitColliders = Physics.OverlapSphere(transform.position, 6);


            for (int i = 0; i < hitColliders.Length; i++)
            {
                Debug.Log(hitColliders[i]);
                if (hitColliders[i].tag == "blast" || hitColliders[i].tag == "move")
                {
                    
                    Destroy(hitColliders[i].gameObject);


                }
                tilePickupAudio.PlayOneShot(jumphigherAudio);
            }


        }

        if (Input.GetButton("Jump") && color == Color.green)
        {
            tilePickupAudio.PlayOneShot(runfasterAudio);
            speed = 120;
        }
        else
        {
            speed = 40;
        }

        if (Input.GetButtonDown("Jump") && (color == Color.yellow)  && powerups.tele_num < 2)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            forward = new Vector3(5 * forward.z, 8, -5 * forward.x);
            powerups.Createtele(transform.position + forward, color);
            teleport = false;

            

        }
        else if (Input.GetButtonDown("Jump") )
        {
            var hitColliders = Physics.OverlapSphere(transform.position, 6);


            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "tele" && powerups.tele_num == 2)
                {
                    float d1 = Vector3.Distance(powerups.yellowbox1.transform.position, transform.position);
                    float d2 = Vector3.Distance(powerups.yellowbox2.transform.position, transform.position);
                    if (d1 < d2 )
                    {
                        tilePickupAudio.PlayOneShot(teleportAudio);

                        transform.position = powerups.yellowbox2.transform.position + new Vector3(-2, 0, 0);
                    }
                    else if (d1 > d2 )
                    {
                        tilePickupAudio.PlayOneShot(teleportAudio);
                        transform.position = powerups.yellowbox1.transform.position + new Vector3(-2, 0, 0);
                    }


                }

                if (hitColliders[i].tag == "Fixedtele")
                {
                    teleController tc = hitColliders[i].GetComponent<teleController>();
                    GameObject other = tc.teleport_other;

                    tilePickupAudio.PlayOneShot(teleportAudio);

                    transform.position = other.transform.position + new Vector3(-2, 0, 0);
                   


                }
            }
        }


        if (Input.GetButtonDown("Restart"))
        {
            gm.RestartLevel();
        }

        if (Input.GetButtonDown("Back"))
        {
            gm.MainMenu();
        }






    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        jump = true;
    
        eatPower(collision);


        if (collision.collider.gameObject.CompareTag("sand")) {
            //if (color != Color.green) {
                jump = false;
                StartCoroutine(dekroy(collision.collider.gameObject));
            //}
        }
    }

    IEnumerator dekroy(GameObject o)
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(o);
    }
    void OnCollisionStay(Collision collision)
    {

        if (Input.GetButton("Jump") && color == Color.blue)
        {
            if (collision.gameObject.GetComponent<Rigidbody>() && collision.gameObject.tag == "move")
            {
                tilePickupAudio.PlayOneShot(pushboxAudio);
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }

        }
    }


    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {




        if (other.gameObject.CompareTag("hole")) {
            gm.LoseGame();

        }
        else if (other.gameObject.CompareTag("finish"))
        {
            Debug.Log("win");
            gm.WinLevel();
        }

   
        // }


    }

    

    public void redPower()
    {
        ChangeColor(Color.red);
        color = Color.red;
        tilePickupAudio.PlayOneShot(redAudio);

    }
   public  void yellowPower()
    {
        ChangeColor(Color.yellow);
        color = Color.yellow;
        tilePickupAudio.PlayOneShot(yellowAudio);
    }

    public void bluePower()
    {
        ChangeColor(Color.blue);
        tilePickupAudio.PlayOneShot(blueAduio);
        color = Color.blue;
     }

    public  void greenPower()
    {
        tilePickupAudio.PlayOneShot(greenAudio);
        ChangeColor(Color.green);
        color = Color.green;
     }

    public void whitePower()
    {
        ChangeColor(Color.white);
        color = Color.white;
    }

    void ChangeColor(Color color)
     {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < 9; i++)
        {
            if (i != 7)
            {
                children[i].material.color = color;
             }

          }
      }



    void eatPower(Collision item)
    {
        if ((item.gameObject.CompareTag("green")) || (item.gameObject.CompareTag("blue"))|| 
            (item.gameObject.CompareTag("red")) || (item.gameObject.CompareTag("yellow")))
        {
            bool check = item.gameObject.GetComponent<SparkController>().eat;
            if (!check)
            {
                return;
            }
        }


        if ( item.gameObject.CompareTag("green")  && color == Color.white)
        {
             item.gameObject.SetActive(false);
             greenPower();
             powerups.count_green--;

        }

        else if ((item.gameObject.CompareTag("blue")  && color == Color.white))
        {
             item.gameObject.SetActive(false);
             bluePower();
             powerups.count_blue--;
 
        }
        else if (item.gameObject.CompareTag("red")  && color == Color.white)
        {
            
            item.gameObject.SetActive(false);
            redPower();
            powerups.count_red--;

        }
        else if (item.gameObject.CompareTag("yellow") && color == Color.white)
        {
             item.gameObject.SetActive(false);
             yellowPower();
            
       
             powerups.count_yellow--;
        }





    }

}
        
     
 

