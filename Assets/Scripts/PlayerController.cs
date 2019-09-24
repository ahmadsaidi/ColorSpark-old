using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            if (Input.GetKey("w"))
            {

                rb.AddForce(0, 0, speed * Time.deltaTime, ForceMode.VelocityChange);

            }
            else if (Input.GetKey("d"))
            {
                rb.AddForce(0.25f*speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            else if (Input.GetKey("a"))
            {
                rb.AddForce(-0.25f*speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            else if (Input.GetKey("s"))
            {
                rb.AddForce(0, 0, -speed * Time.deltaTime, ForceMode.VelocityChange);
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
            time = 2;
            ChangeColor(Color.white);
        }

    }

    void OnTriggerEnter(Collider other) 
    {
        if (!fast && !ghost && !highJump)
        {
            if (other.gameObject.CompareTag("green"))
            {
                other.gameObject.SetActive(false);
                tilePickupAudio.PlayOneShot(fastAudio);
                ChangeColor(Color.green);
                speedLimit = 200;
                fast = true;
            }
            if (other.gameObject.CompareTag("blue"))
            {
                other.gameObject.SetActive(false);
                ChangeColor(Color.blue);
                powerups.ghost(false);
                ghost = true;
                tilePickupAudio.PlayOneShot(ghostAudio);
            }
            if (other.gameObject.CompareTag("red"))
            {
                other.gameObject.SetActive(false);
                ChangeColor(Color.red);
                jumpspeed = 20000;
                highJump = true;
                tilePickupAudio.PlayOneShot(highJumpAudio);
            }
        }


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
