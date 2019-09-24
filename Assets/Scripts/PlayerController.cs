using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;

    public float speed;
    public AudioSource goodpick;
    Material m_Material;
    public float jumpspeed = 10000;
    float time = 2;
    bool fast, ghost, highJump = false;
    public float speedLimit = 100;
    bool canjump = true;
    PowerUps powerups;
    public AudioClip ghostAudio, fastAudio, highJumpAudio;
    public AudioSource tilePickupAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 1000;
        rb.freezeRotation = true;
        m_Material = GetComponent<Renderer>().material;
        powerups = rb.gameObject.GetComponent<PowerUps>();
    }

    void reset_timer()
    {
        time = 2;
    }

    private void Update()
    {
        if (transform.position.y < -10) {
            Application.Quit();
        }
        if (Input.GetKeyDown("space") && canjump)
        {
            rb.AddForce(0, jumpspeed, 0, ForceMode.Impulse);
            canjump = false;
        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
        {
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.magnitude < speedLimit)
        {
            // w and s cannot be pressed at the same time
            // however w and a/d can be pressed at the same time
            if (Input.GetKey("w"))
            {
                rb.AddForce(0, 0, speed * Time.deltaTime, ForceMode.VelocityChange);
            }
            else if (Input.GetKey("s"))
            {
                rb.AddForce(0, 0, -speed * Time.deltaTime, ForceMode.VelocityChange);
            }
            if (Input.GetKey("d"))
            {
                rb.AddForce(0.3f*speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            } 
            else if (Input.GetKey("a"))
            {
                rb.AddForce(-0.3f*speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            
        }
        

        if (fast || ghost || highJump)
        {
            time -= Time.fixedDeltaTime;
        }
        if (time <= 0)
        {
            fast = false;
            ghost = false;
            highJump = false;
            speedLimit = 100;
            jumpspeed = 10000;
            powerups.ghost(true);
            reset_timer();
            ChangeColor(Color.white);
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
            if (other.gameObject.CompareTag("green"))
            {
                other.gameObject.SetActive(false);
                tilePickupAudio.PlayOneShot(fastAudio);
                ChangeColor(Color.green);
                speedLimit = 200;
                fast = true;
            }
            else if (other.gameObject.CompareTag("blue"))
            {
                other.gameObject.SetActive(false);
                ChangeColor(Color.blue);
                powerups.ghost(false);
                ghost = true;
                tilePickupAudio.PlayOneShot(ghostAudio);
            }
            else if (other.gameObject.CompareTag("red"))
            {
                other.gameObject.SetActive(false);
                ChangeColor(Color.red);
                jumpspeed = 30000;
                highJump = true;
                tilePickupAudio.PlayOneShot(highJumpAudio);
                SceneManager.LoadScene("Lose");
            }else if (other.gameObject.CompareTag("trap")){
                SceneManager.LoadScene("Lose");
            }
            reset_timer();
        // }


    }

    void OnCollisionEnter(Collision collision)
    {
        canjump = true;
    }

    void ChangeColor(Color color)
    {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            rend.material.color = color;
        }
    }

}
