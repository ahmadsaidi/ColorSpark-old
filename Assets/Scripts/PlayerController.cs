﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;

    public float speed;
    public float rotationSpeed;
    public float jumpspeed = 7000;
    public Color color = Color.white;
    PowerUps powerups;
    public Light led;
    public AudioSource tilePickupAudio;
    bool eat = false;
    bool jump = true;
    public bool carry = false;
    public GameObject pauseMenu;
    public RobotIcon Icon;
    bool paused = false;
    GameObject carryThing;
    GameManager gm;
    MusicManager mm;
    float currVerRot = 0;
    public Transform axel;
    Animator animator;
    bool stationary = true;
    //public WheelCollider leftwheel;
    //public WheelCollider rightwheel;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 40;
        rotationSpeed = 75;
        rb.freezeRotation = true;
        powerups = rb.gameObject.GetComponent<PowerUps>();
        gm = FindObjectOfType<GameManager>();
        mm = FindObjectOfType<MusicManager>();
        tilePickupAudio = GetComponent<AudioSource>();
        Icon = FindObjectOfType<RobotIcon>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (transform.position.y < -100)
        {
            gm.LoseGame();

        }
        float translationx = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        float rotationv = Input.GetAxis("Camera Vertical") * rotationSpeed;
        float rotationh = Input.GetAxis("Camera Horizontal") * rotationSpeed;

        //float motor = translationx*100;
        //axel.Rotate(0,0, -0.1f * translationx);
        translationx *= Time.deltaTime;
        rotationv *= Time.deltaTime;
        rotationh *= Time.deltaTime;
        rotation *= Time.deltaTime;


        transform.Translate(0 , 0, translationx);
        transform.Rotate(0, rotation, 0);

        if (stationary && translationx != 0)
        {
            if (speed == 80)
            {
                animator.SetTrigger("startedSprinting");
            } else {
                animator.SetTrigger("startedWalking");
            }
        }
        stationary = translationx == 0;
        //float angle = Mathf.PI * transform.rotation.eulerAngles.y / 180;
        //if (rb.velocity.magnitude < speed)
        //{
        //    rb.AddForce( 10 * translationx * Mathf.Cos(angle),0, -10 * translationx * Mathf.Sin(angle), ForceMode.VelocityChange);
        //}
        //if (translationx < 0.1 && translationx > -0.1)
        //{
        //    rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.01f, 1, 0.01f));
        //}        //Debug.Log(angle);
        //float steering = rotation;

        //leftwheel.steerAngle = steering;
        //rightwheel.steerAngle = steering;
        //leftwheel.motorTorque = motor;
        //rightwheel.motorTorque = motor;

        

        if (rotationv != 0 && (currVerRot < 10 && currVerRot > -10))
        {
            currVerRot += rotationv;
            led.transform.Rotate(rotationv, 0, 0);
        } else if (rotationv == 0 && (currVerRot > 0.01 || currVerRot < -0.01))
        {
            led.transform.Rotate(-currVerRot/10, 0, 0);
            currVerRot -= currVerRot / 10;
        } else if (rotationv == 0 && currVerRot < 0.01 && currVerRot > -0.01)
        {
            led.transform.Rotate(-currVerRot, 0, 0);
            currVerRot = 0;
        }


            if (Input.GetButtonDown("Fire1") && jump == true && paused == false)
        {
            // red for high jump
            //if (color == Color.red) {
            //   rb.AddForce(Vector3.up * 1.3f * jumpspeed);
            //}
            //else
            //{
            rb.AddForce(Vector3.up * jumpspeed);
            tilePickupAudio.PlayOneShot(mm.jump);
            animator.SetTrigger("startedJumping");
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

        if (Input.GetButtonDown("Fire3") && carry == false)
        {
            var hitColliders = Physics.OverlapSphere(transform.position , 5);


            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i].tag == "move")
                {

                    carryThing = (hitColliders[i].gameObject);
                    carry = true;

                }
                //tilePickupAudio.PlayOneShot(mm.blastAudio);
            }
        } else if (Input.GetButtonDown("Fire3") && carry )
        {
            var hitColliders = Physics.OverlapSphere(transform.position, 5);
            if ( hitColliders.Length  < 4)
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                forward = new Vector3(3 * forward.z, 2, -3 * forward.x);
                carryThing.transform.position = transform.position + forward;
                carry = false;
            }
            
        }

        if (carry && carryThing)
        {
            carryThing.transform.position = transform.position + new Vector3(0, 15, 0);
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
                //tilePickupAudio.PlayOneShot(mm.blastAudio);
            }


        }

        if (Input.GetButton("Jump") && color == Color.green)
        {
            tilePickupAudio.PlayOneShot(mm.runfasterAudio);
            speed = 80;
        }
        else
        {
            speed = 40;
        }

        if (Input.GetButtonDown("Jump") && (color == Color.blue)  && powerups.tele_num < 2)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            forward = new Vector3(5 * forward.z, 8, -5 * forward.x);
            powerups.Createtele(transform.position + forward, color);





        }
        else if (Input.GetButtonDown("Jump"))
        {
            var hitColliders = Physics.OverlapSphere(transform.position, 6);


            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "tele" && powerups.tele_num == 2)
                {
                    float d1 = Vector3.Distance(powerups.yellowbox1.transform.position, transform.position);
                    float d2 = Vector3.Distance(powerups.yellowbox2.transform.position, transform.position);
                    if (d1 < d2)
                    {
                        tilePickupAudio.PlayOneShot(mm.teleportAudio);

                        transform.position = powerups.yellowbox2.transform.position + new Vector3(-2, 0, 0);
                    }
                    else if (d1 > d2)
                    {
                        tilePickupAudio.PlayOneShot(mm.teleportAudio);
                        transform.position = powerups.yellowbox1.transform.position + new Vector3(-2, 0, 0);
                    }


                }

                if (hitColliders[i].tag == "Fixedtele")
                {
                    teleController tc = hitColliders[i].GetComponent<teleController>();
                    GameObject other = tc.teleport_other;

                    tilePickupAudio.PlayOneShot(mm.teleportAudio);

                    transform.position = other.transform.position + new Vector3(-2, 0, 0);



                }
            }
        }



        if (Input.GetButtonDown("Restart"))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            paused = true;
        }

        if (Input.GetButtonDown("Fire1") && paused)
        {
            pauseMenu.SetActive(false);
            jump = true;
            Time.timeScale = 1;
            paused = false;

        }

        if (Input.GetButtonDown("Fire2") && paused)
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
            gm.RestartLevel();
        }

        if (Input.GetButtonDown("Fire3") && paused)
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
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
                tilePickupAudio.PlayOneShot(mm.pushboxAudio);
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }

        }
        //This is to make sure the colliders update
        if (collision.gameObject.CompareTag("float"))
        {
            transform.Translate(0, 0.01f, 0);
        }
        // stop people from going through walls hopefully
        if (collision.gameObject.CompareTag("wall"))
        {
            transform.Translate(-0.1f, 0, -0.1f);
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
        tilePickupAudio.PlayOneShot(mm.redAudio);
        Icon.GetComponent<Image>().color = Color.white;
        Icon.GetComponent<Image>().sprite = Icon.Drill;

    }


    public void bluePower()
    {
        ChangeColor(Color.blue);
        tilePickupAudio.PlayOneShot(mm.blueAudio);
        color = Color.blue;
        Icon.GetComponent<Image>().color = Color.white;
        Icon.GetComponent<Image>().sprite = Icon.Teleport;
    }

    public  void greenPower()
    {
        tilePickupAudio.PlayOneShot(mm.greenAudio);
        ChangeColor(Color.green);
        color = Color.green;
        Icon.GetComponent<Image>().color = Color.white;
        Icon.GetComponent<Image>().sprite = Icon.Rocket;
    }

    public void whitePower()
    {
        ChangeColor(Color.white);
        color = Color.white;
        Icon.GetComponent<Image>().color = Color.black;
        Icon.GetComponent<Image>().sprite = null;
    }

    void ChangeColor(Color color)
     {
        led.color = color;
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






    }

}
        
     
 

