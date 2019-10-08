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
    public AudioClip ghostAudio, fastAudio, highJumpAudio;
    public AudioSource tilePickupAudio;
    bool eat = false;
    bool jump = true;
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
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        // green for speed up
        if (color == Color.green) {
            translation *= 2;
        }
        transform.Translate(translation,0, 0 );
        transform.Rotate( 0,rotation, 0);

        if (Input.GetButtonDown("Fire1") && jump == true)
        {
            // red for high jump
            if (color == Color.red) {
                rb.AddForce(Vector3.up * 2 * jumpspeed);
            }
            else
            {
                rb.AddForce(Vector3.up * jumpspeed);
            }
            jump = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            // get forward direciton
            Vector3 forward = transform.TransformDirection (Vector3.forward);
            forward = new Vector3(5*forward.z, 3, -5*forward.x);
            powerups.Createbox(transform.position + forward, color);
        }

        if (Input.GetKey("r"))
        {
            gm.RestartLevel();
        }

        //if (Input.GetButtonDown("Jump") && (color == Color.red) && jump == true)
        //{
        //    float newspeed = jumpspeed * 2;
        //     rb.AddForce(Vector3.up * newspeed );
        //    jump = false;

        //}






    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        jump = true;

        if (Input.GetButtonDown("Fire3"))
        {
            eat = true;
        }
        eatPower(collision);
        eat = false;

        if (Input.GetButtonDown("Jump"))
        {
            if (collision.gameObject.CompareTag("yellowbox") && powerups.count_yellow == 2)
            {
                float d1 = Vector3.Distance(powerups.yellow1, transform.position);
                float d2 = Vector3.Distance(powerups.yellow2, transform.position);
                if (d1 < d2)
                {
                    transform.position = powerups.yellow2 + new Vector3(0, 4, 0);
                }
                else
                {
                    transform.position = powerups.yellow1 + new Vector3(0, 4, 0);
                }

            }
        }
        if (Input.GetButton("Jump") && color == Color.blue)
        {
            Debug.Log("get here");
            if (collision.gameObject.GetComponent<Rigidbody>())
            {
                Debug.Log("get there");
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }

        }

        if (collision.collider.gameObject.CompareTag("sand")) {
            if (color != Color.green) {
                jump = false;
                Destroy(collision.collider.gameObject);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {

        if (Input.GetButtonDown("Fire3"))
        {
            eat = true;
        }
        eatPower(collision);
        eat = false;

        if (Input.GetButtonDown("Jump"))
        {
            if (collision.gameObject.CompareTag("yellowbox") && powerups.count_yellow == 2)
            {
                float d1 = Vector3.Distance(powerups.yellow1, transform.position);
                float d2 = Vector3.Distance(powerups.yellow2, transform.position);
                if (d1 < d2)
                {
                    transform.position = powerups.yellow2 + new Vector3(0, 4, 0);
                }
                else
                {
                    transform.position = powerups.yellow1 + new Vector3(0, 4, 0);
                }

            }
        }
        if (Input.GetButton("Jump") && color == Color.blue)
        {
            if (collision.gameObject.GetComponent<Rigidbody>())
            {
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
        // if (!fast && !ghost && !highJump)
        // {
        // Once a player hit a color grid
        // no matter what state the player is in
        // the player changes state immediately after hitting the grid
        // and timer resets
        Debug.Log(eat);
        getPower(other);


        if (other.gameObject.CompareTag("trap")) {
            gm.LoseGame();

        }
        else if (other.gameObject.CompareTag("finish"))
        {
            Debug.Log("win");
            gm.WinGame();
        }

   
        // }


    }

    

    void redPower()
    {
        ChangeColor(Color.red);
        tilePickupAudio.PlayOneShot(highJumpAudio);
        color = Color.red;
    }
    void yellowPower()
    {
        ChangeColor(Color.yellow);
        color = Color.yellow;
    }

    void bluePower()
    {
        ChangeColor(Color.blue);
        tilePickupAudio.PlayOneShot(ghostAudio);
        color = Color.blue;
     }

    void greenPower()
    {
        tilePickupAudio.PlayOneShot(fastAudio);
        ChangeColor(Color.green);
        color = Color.green;
     }

    void whitePower()
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

    void getPower(Collider item)
    {
        if (item.gameObject.CompareTag("green") )
        {
             greenPower();
        }

        else if (item.gameObject.CompareTag("blue") )
        {
            bluePower();
        }
        else if (item.gameObject.CompareTag("red") )
        {
            
             redPower();
            

        }
        else if (item.gameObject.CompareTag("yellow") )
        {
            yellowPower();
        }
        else if (item.gameObject.CompareTag("white"))
        {
           whitePower();
        }
       

     }

    void eatPower(Collision item)
    {
        if ( item.gameObject.CompareTag("greenbox") && eat)
        {
             item.gameObject.SetActive(false);
             //greenPower();
        }

        else if ((item.gameObject.CompareTag("bluebox") && eat))
        {
             item.gameObject.SetActive(false);
             //bluePower();
            
        }
        else if (item.gameObject.CompareTag("redbox") && eat)
        {
            
            item.gameObject.SetActive(false);
            //redPower();
        }
        else if (item.gameObject.CompareTag("yellowbox") && eat)
        {
             item.gameObject.SetActive(false);
             //yellowPower();
             powerups.count_yellow--;

        }
        else if (item.gameObject.CompareTag("whitebox") && eat)
        {
             item.gameObject.SetActive(false);
             //whitePower();
        }


    }

}
        
     
 

