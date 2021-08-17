using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public ParticleSystem particles;
    private bool done = false;
    public bool stop = false;
    public AudioSource A;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<MineBoss>() && !done)
            smash(other.gameObject);
        if (stop && other.gameObject.tag != "CantHook" )
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            particles.loop = false;
            
            this.enabled = false;
        }
    }

    void smash(GameObject other)
    {
        done = true;
        other.gameObject.GetComponent<MineBoss>().hurt();
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        particles.Play();
        A.Play();
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }
}
