 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool activated;
    public bool iswin = false;
    public bool explosion = false;
    public GameObject explosiong;
    public GameObject inactiveflag;
    public GameObject activeflag;
    public ParticleSystem celebration;
    public AudioSource powerupaudiosource;
    public Timer master;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Movement>() && !activated)
        {
            celebration.Play();
            inactiveflag.SetActive(false);
            activeflag.SetActive(true);
            other.gameObject.GetComponent<Movement>().respawpoint = this.gameObject;
            activated = true;
            powerupaudiosource.volume = PlayerPrefs.GetFloat("SFX");
            powerupaudiosource.Play();
            
            if(iswin)
                master.complete();

            if (explosion)
            {
                explosiong.SetActive(true);
            }

        }
    }
}
