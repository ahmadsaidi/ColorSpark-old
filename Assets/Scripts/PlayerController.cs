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
    public float jumpspeed = 500000;
    bool fast, teleport, highJump,push = false;
    public float speedLimit = 100;
    bool canjump = true;
    public Color color = Color.white;
    PowerUps powerups;
    public AudioClip ghostAudio, fastAudio, highJumpAudio;
    public AudioSource tilePickupAudio;
    bool eat = false;
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
        transform.Translate(translation,0, 0 );
        transform.Rotate( 0,rotation, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            rb.AddForce(Vector3.up * jumpspeed);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            powerups.Createbox(transform.position, color);
        }

 


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetButtonDown("Fire3"))
        {
           // eat = true;
        }





    }

    void OnCollisionEnter(Collision collision)
    {
       

        canjump = true;
        if (collision.collider.gameObject.CompareTag("sand"))
        {
            Destroy(collision.collider.gameObject);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log(eat);
        if (Input.GetButtonDown("Fire3"))
        {
            eat = true;
        }
        eatPower(collision);
        eat = false;
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
        jumpspeed = 1000000;
        highJump = true;
        fast = false;
        push = false;
        teleport = false;
        tilePickupAudio.PlayOneShot(highJumpAudio);
        color = Color.red;
    }
    void yellowPower()
    {
        ChangeColor(Color.yellow);
        highJump = false;
        fast = false;
        push = false;
        teleport = true;
        color = Color.yellow;
    }

    void bluePower()
    {
        ChangeColor(Color.blue);
        push = true;
        tilePickupAudio.PlayOneShot(ghostAudio);
        fast = false;
        highJump = false;
        teleport = false;
        color = Color.blue;
     }

    void greenPower()
    {
        tilePickupAudio.PlayOneShot(fastAudio);
        ChangeColor(Color.green);
        speedLimit = 200;
        fast = true;
        highJump = false;
        teleport = false;
        push = false;
        color = Color.green;
     }

    void whitePower()
    {

        ChangeColor(Color.white);
        speedLimit = 100;
        fast = false;
        highJump = false;
        teleport = false;
        push = false;
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
             greenPower();
        }

        else if ((item.gameObject.CompareTag("bluebox") && eat))
        {
             item.gameObject.SetActive(false);
             bluePower();
            
        }
        else if (item.gameObject.CompareTag("redbox") && eat)
        {
            
            item.gameObject.SetActive(false);
            redPower();
        }
        else if (item.gameObject.CompareTag("yellowbox") && eat)
        {
             item.gameObject.SetActive(false);
             yellowPower();

        }
        else if (item.gameObject.CompareTag("whitebox") && eat)
        {
             item.gameObject.SetActive(false);
             whitePower();
        }


    }
}
        
     
 

