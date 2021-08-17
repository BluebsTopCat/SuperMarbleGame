using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudioMixing : MonoBehaviour
{
    public float volume;
    private Movement player;
    public float speed;
    public float maxspeed;
    public float volumemult;
    public float pitchmult;
    public AudioSource groundloop;
    public AudioSource jumpland;
    public AudioSource EntSplash;
    public AudioSource ExSplash;
    public bool inair;
    private bool inairlastframe;

    public AudioClip jump;

    public AudioClip land;

    private float airtime;
    // Update is called once per frame
    private void Start()
    {
        volume = PlayerPrefs.GetFloat("SFX");
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    void Update()
    {
        inairlastframe = inair;
        speed = player.circle.velocity.x;
        maxspeed = player.movementspeed;
        inair = !player.touchingground;
        if (inair || player.playerfrozen)
            groundloop.volume = 0;
        else
        {
            groundloop.volume = (Mathf.Abs(speed) / maxspeed) * volume;
            groundloop.pitch = 1 + (Mathf.Abs(speed) / maxspeed) * pitchmult;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !inair && !player.playerfrozen)
        {
            jumpland.pitch = Random.Range(.9f, 1.1f);
            jumpland.clip = jump;
            jumpland.Play();
        }

        if (inairlastframe && inair == false)
        {
            jumpland.pitch = Random.Range(.9f, 1.1f);
            jumpland.volume = (.6f + airtime/2)* volume;
            jumpland.clip = land;
            jumpland.Play();
        }

        airtime = player.currentcoyotetime;

    }
}
