using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class water : MonoBehaviour
{
    public bool clampx;
    public GameObject player;

    private Movement playermovement;

    public Rigidbody2D playerrb;

    public GameObject splash;

    private ParticleSystem splashps;

    public AudioSource entersplash;

    public AudioSource exitsplash;

    private float splashsfx;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playermovement = player.GetComponent<Movement>();
        playerrb = player.GetComponent<Rigidbody2D>();
        splashps = splash.GetComponent<ParticleSystem>();
        splashsfx = PlayerPrefs.GetFloat("SFX");
        entersplash.volume = splashsfx;
        exitsplash.volume = splashsfx;
    }

    private void Update()
    {
        if (clampx)
        {
            this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerrb.gravityScale = playermovement.watergravity;
            playerrb.drag = 2f;
            playerrb.angularDrag = 2f;
            playermovement.inwater = true;
            playermovement.jumpedinwater = false;
            float speed = Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x/2) +
                          Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.y/2);
            if (speed > 3 && playermovement.timeoutofwater > .2f)
            {    
                splash.transform.rotation =
                    Quaternion.LookRotation(-other.gameObject.GetComponent<Rigidbody2D>().velocity);
                splash.transform.position =
                    other.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position);
                splash.SetActive(true);

                splashps.emission.SetBurst(0, new ParticleSystem.Burst(0.0f, speed * 20));
                splashps.Play();
                entersplash.pitch = UnityEngine.Random.Range(.7f, 1.2f);
                entersplash.Play();
            }
        }
        else if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            other.GetComponent<Rigidbody2D>().gravityScale = -1f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerrb.gravityScale = playermovement.regulargravity;
            playerrb.drag = .01f;
            playerrb.angularDrag = 0;
            playermovement.inwater = false;
            float speed = Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x/2) +
                          Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.y/2);
            if (speed > 3 && playermovement.timeinwater > .2f) 
            {
                splash.transform.rotation  = Quaternion.LookRotation(other.gameObject.GetComponent<Rigidbody2D>().velocity);
                splash.transform.position = other.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position);
                splash.SetActive(true);
                splashps.emission.SetBurst(0, new ParticleSystem.Burst(0.0f, speed * 20));
                splashps.Play();
                exitsplash.pitch = UnityEngine.Random.Range(.7f, 1.2f);
                exitsplash.Play();
            }
        }
        else if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
    
}
