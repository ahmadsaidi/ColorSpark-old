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
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        // green for speed up
        //if (color == Color.green) {
         //   translation *= 2;
       // }
        transform.Translate(translation,0, 0 );
        transform.Rotate( 0,rotation, 0);

        if (Input.GetButtonDown("Fire1") && jump == true)
        {
            // red for high jump
            //if (color == Color.red) {
             //   rb.AddForce(Vector3.up * 2 * jumpspeed);
           // }
           // else
           // {
             rb.AddForce(Vector3.up * jumpspeed);
          //  }
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
            eat = true;
            powerups.GetEnginePower(transform.position);
            eat = false;
        }

        if (Input.GetKey("r"))
        {
            gm.RestartLevel();
        }

        if (Input.GetButtonDown("Jump") && (color == Color.red) && jump == true)
        {
            float newspeed = jumpspeed * 1.3f;
             rb.AddForce(Vector3.up * newspeed );
            jump = false;

        }

        if (Input.GetButton("Jump") && color == Color.green)
        {
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
    
       

        if (Input.GetButton("Fire3"))
        {
            eat = true;
        }
        eatPower(collision);
        eat = false;

        if (Input.GetButton("Jump") && powerups.tele_num == 2)
        {

            if (collision.gameObject.CompareTag("tele"))
            {

                float d1 = Vector3.Distance(powerups.yellowbox1.transform.position, transform.position);
                float d2 = Vector3.Distance(powerups.yellowbox2.transform.position, transform.position);
                if (d1 < d2 && teleport)
                {
                    transform.position = powerups.yellowbox2.transform.position + new Vector3(-2, 0, 0);
                }
                else if (d1 > d2 && teleport)
                {
                    transform.position = powerups.yellowbox1.transform.position + new Vector3(-2, 0, 0);
                }
                teleport = false;
            }
        }
        else if (powerups.tele_num == 2)
        {
            teleport = true;
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

        if (Input.GetButtonDown("Fire3"))
        {
            eat = true;
        }
        eatPower(collision);
        eat = false;

        if (Input.GetButtonDown("Jump") && powerups.tele_num == 2)
        {
            if (collision.gameObject.CompareTag("tele") )
            {

                float d1 = Vector3.Distance(powerups.yellowbox1.transform.position, transform.position);
                float d2 = Vector3.Distance(powerups.yellowbox2.transform.position, transform.position);
                if (d1 < d2 && teleport)
                {
                    transform.position = powerups.yellowbox2.transform.position + new Vector3(-2, 0, 0);
                }
                else if (d1 > d2 && teleport)
                {
                    transform.position = powerups.yellowbox1.transform.position + new Vector3(-2, 0, 0);
                }
                teleport = false;
            }
        }
        else if (powerups.tele_num == 2)
        {
            teleport = true;
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

        Debug.Log(eat);



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
        tilePickupAudio.PlayOneShot(highJumpAudio);
        color = Color.red;
        
    }
   public  void yellowPower()
    {
        ChangeColor(Color.yellow);
        color = Color.yellow;
    }

    public void bluePower()
    {
        ChangeColor(Color.blue);
        tilePickupAudio.PlayOneShot(ghostAudio);
        color = Color.blue;
     }

    public  void greenPower()
    {
        tilePickupAudio.PlayOneShot(fastAudio);
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
        if ( item.gameObject.CompareTag("green") && eat)
        {
             item.gameObject.SetActive(false);
             greenPower();
             powerups.count_green--;
        }

        else if ((item.gameObject.CompareTag("blue") && eat))
        {
             item.gameObject.SetActive(false);
             bluePower();
             powerups.count_blue--;
        }
        else if (item.gameObject.CompareTag("red") && eat)
        {
            
            item.gameObject.SetActive(false);
            redPower();
            powerups.count_red--;
        }
        else if (item.gameObject.CompareTag("yellow") && eat)
        {
             item.gameObject.SetActive(false);
             yellowPower();
            // if there are two existing box, eat one
            if (powerups.count_yellow == 2)
            {
                if (item.gameObject.GetInstanceID() == powerups.yellowbox1.GetInstanceID())
                {
                    // if box1 is eaton, assign box2 to box1 then decrement
                    powerups.yellowbox1 = powerups.yellowbox2;
                    powerups.yellowbox2 = null;
                }
                else {
                    // if box2 is eaton just decrement 
                    powerups.yellowbox2 = null;
                }
            }
            // eat whatever is left
            else {
                powerups.yellowbox1 = null;
            }
             powerups.count_yellow--;
        }





    }

}
        
     
 

