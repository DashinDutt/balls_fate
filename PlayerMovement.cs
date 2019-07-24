

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float upwardForce = 30000f;
    public float sidewaysForce = 500f;
    public float zaxis = 500f;
    public Vector3 startPos;
    public float distToGround;
    public bool isGrounded;
    public GameObject barrelprefab;
    public GameObject explosionball;
    public GameObject barrelexplosion;
    public AudioClip barrelcol;
    public AudioSource barrelcollect;
    public AudioClip playerfly;
    public AudioSource playerflying;
    public AudioClip playerjump;
    public AudioSource playerjumping;
    public int playerscore;
    public Text score;
    public double playerhealth;
    public Text health;
    public bool isSlow;
    public float vel;
    public int playerlives;
    public Text lives;
    public bool energyiszero;

    public void SpawnBar()
    {
        Vector3 pos = new Vector3(Random.Range(-50f, 50f), 70f, Random.Range(-50f, 50f));
        Vector3 angle = new Vector3(Random.Range(0f, 359f), Random.Range(0f, 359f), Random.Range(0f, 359f));
        Instantiate(barrelprefab, pos, Quaternion.Euler(new Vector3(Random.Range(0f, 359f), Random.Range(0f, 359f), Random.Range(0f, 359f))));
        
    }

    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.name == "Cylinder(Clone)")
        {
            Instantiate(barrelexplosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            barrelcollect.Play();
            Destroy(collision.gameObject);
            playerscore++;
            score.text = "SCORE: " + playerscore;
            if ((playerhealth + 25) > 100)
                playerhealth = 100;
            else
                playerhealth += 25;
        }
        if (collision.gameObject.name == "Cylinder(Clone)(Clone)")
            Destroy(collision.gameObject);
            

    }
    void Start()
    {
        startPos = rb.transform.position;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        barrelcollect.clip = barrelcol;
        playerflying.clip = playerfly;
        playerjumping.clip = playerjump;
        playerscore = 0;
        playerhealth = 100;
        playerlives = 10;
    }
    
   

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerlives == 0)
        {
            
            Destroy(rb.gameObject);
            FindObjectOfType<GameManager>().EndGame();


        }
        else
        {
            energyiszero = ((int)playerhealth == 0);
            lives.text = "BALLS REMAINING: " + playerlives;
            health.text = "ENERGY: " + (int)playerhealth;
            vel = rb.velocity.magnitude;
            isSlow = vel < 30f;

           
            isGrounded = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.8f);
            /*
             * while ((!isGrounded)&&(!playerflying.isPlaying))
            {
                playerflying.Play();
            }

            if (isGrounded && playerflying.isPlaying)
                playerflying.Stop();
            */
            if (energyiszero)
            {
                Instantiate(explosionball, rb.transform.position, rb.rotation);
                if (!(playerlives - 1 < 0))
                    playerlives--;
                else
                    playerlives = 0;
                playerhealth = 100;
            }
            
            if (isSlow)
            {
                if (!(playerhealth - 0.1 < 0))
                    playerhealth -= 0.8;
                else
                    playerhealth = 0;
            }
            else
            {
                if (!(playerhealth + 0.3 > 100))
                    playerhealth += 0.3;
                else
                    playerhealth = 100;
            }
            if (Input.GetKey("d"))
            {
                rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
            }

            if (Input.GetKeyDown("space") && isGrounded)
            {

                if (!playerjumping.isPlaying)
                    playerjumping.Play();
                Instantiate(explosionball, rb.transform.position, rb.rotation);
                rb.AddForce(0, upwardForce * Time.deltaTime, 0);



            }

            if (Input.GetKey("left ctrl") && !isGrounded)
            {

                rb.AddForce(0, -upwardForce * 0.1f * Time.deltaTime, 0);
                

            }

            if (Input.GetKey("w"))
            {
                rb.AddForce(0, 0, zaxis * Time.deltaTime);
            }

            if (Input.GetKey("s"))
            {
                rb.AddForce(0, 0, -zaxis * Time.deltaTime);
            }

            if (rb.transform.position[1] < -10)
            {
                rb.transform.position = startPos;
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.angularVelocity = new Vector3(0f, 0f, 0f);
                if (!(playerlives - 1 < 0))
                    playerlives--;
                else
                    playerlives = 0;
                playerhealth = 100;

            }
            if (rb.transform.position[1] > 80)
            {
                rb.velocity = rb.velocity - new Vector3(0, 10f, 0);
            }
        }

       

    }
}
