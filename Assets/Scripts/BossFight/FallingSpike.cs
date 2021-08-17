using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FallingSpike : MonoBehaviour
{
    public ParticleSystem thingy;
    public AudioSource A;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
            other.gameObject.GetComponent<Movement>().externalpk();

        
        if(other.gameObject.GetComponent<MineBoss>())
            other.gameObject.GetComponent<MineBoss>().donk();
        if (!other.gameObject.CompareTag("CantHook"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            this.GetComponent<SpriteRenderer>().sprite = null;

            StartCoroutine(Explode());
        }
    }

    public IEnumerator Explode()
    {
        thingy.Play();
        A.pitch = UnityEngine.Random.Range(.75f, 1.25f);
        A.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
