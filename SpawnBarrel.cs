using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBarrel : MonoBehaviour
{
    //public GameObject currentcyl;
    public GameObject Barrelprefab;
    public GameObject explosion;
    public GameObject barrelexplosion;
    public AudioClip barrelhit;
    public AudioSource barrelhitting;
    public int surfacehealth;
    public Text health;
    public GameObject Ball;
    // Start is called before the first frame update
    public void Start()
    {
        SpawnBar();
        barrelhitting.clip = barrelhit;
        surfacehealth = 20;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        health.text = "SURFACE HEALTH: " + surfacehealth;
        if (!GameObject.Find("Cylinder(Clone)"))
            SpawnBar();
        if(surfacehealth==0)
        {
            Destroy(Ball);
            FindObjectOfType<GameManager>().EndGame();
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.name == "Cylinder(Clone)")
        {
            Instantiate(barrelexplosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            barrelhitting.Play();
            Destroy(collision.gameObject);
            if (surfacehealth - 1 < 0)
                surfacehealth = 0;
            else
                surfacehealth--; 
           
        }
        if (collision.gameObject.name == "Cylinder(Clone)(Clone)")
            Destroy(collision.gameObject);

    }

    public void SpawnBar()
    {
        Vector3 pos = new Vector3(Random.Range(-50f, 50f), 70f, Random.Range(-50f, 50f));
        Vector3 angle = new Vector3(Random.Range(0f, 359f), Random.Range(0f, 359f), Random.Range(0f, 359f));
        Instantiate(Barrelprefab, pos, Quaternion.Euler(new Vector3(Random.Range(0f, 359f), Random.Range(0f, 359f), Random.Range(0f, 359f))));
    }
}

