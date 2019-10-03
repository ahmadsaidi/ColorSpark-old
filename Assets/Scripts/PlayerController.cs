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
    Material m_Material;
    public float jumpspeed = 10000;
    float time = 2;
    bool fast, ghost, highJump = false;
    public float speedLimit = 100;
    bool canjump = true;
    PowerUps powerups;
    public AudioClip ghostAudio, fastAudio, highJumpAudio;
    public AudioSource tilePickupAudio;
    GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 40;
        rotationSpeed = 100;
        rb.freezeRotation = true;
        m_Material = GetComponent<Renderer>().material;
        powerups = rb.gameObject.GetComponent<PowerUps>();
        gm = FindObjectOfType<GameManager>();
    }

    void reset_timer()
    {
        time = 2;
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

        if (Input.GetButton("Fire1"))
        {
            rb.AddForce(0, 100, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


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
            //other.gameObject.SetActive(false);
            tilePickupAudio.PlayOneShot(fastAudio);
            ChangeColor(Color.green);
            speedLimit = 200;
            fast = true;
            highJump = false;
            ghost = false;
            powerups.ghost(true);
        }
        else if (other.gameObject.CompareTag("blue"))
        {
            //other.gameObject.SetActive(false);
            ChangeColor(Color.blue);
            powerups.ghost(false);
            ghost = true;
            tilePickupAudio.PlayOneShot(ghostAudio);
            fast = false;
            highJump = false;
        }
        else if (other.gameObject.CompareTag("red"))
        {
            //other.gameObject.SetActive(false);
            ChangeColor(Color.red);
            jumpspeed = 30000;
            highJump = true;
            fast = false;
            ghost = false;
            powerups.ghost(true);
            tilePickupAudio.PlayOneShot(highJumpAudio);
        }
        else if (other.gameObject.CompareTag("trap")) {
            gm.LoseGame();

        }
        else if (other.gameObject.CompareTag("finish"))
        {
            Debug.Log("win");
            gm.WinGame();
        }

        reset_timer();
        // }


    }

    void OnCollisionEnter(Collision collision)
    {
        canjump = true;
        if (collision.collider.gameObject.CompareTag("sand")) {
            Destroy(collision.collider.gameObject);
        } 
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