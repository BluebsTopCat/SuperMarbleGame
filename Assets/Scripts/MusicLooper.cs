using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLooper : MonoBehaviour
{
    private int level = 4;

    public AudioSource[] a;
    public AudioSource b;

    public bool multipletracks = true;

    public bool volumestuff = true;
    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        if(volumestuff)
        {
         foreach (AudioSource a in a)
         {
            
            a.volume = PlayerPrefs.GetFloat("MUSIC") * .5f;
         }
        }
        if (multipletracks)
        {
            if (volumestuff)
            {
                b.volume = PlayerPrefs.GetFloat("MUSIC") * .5f;
            }

            foreach (AudioSource a in a)
            {
                a.Play();
                Invoke(nameof(switchtrack), a.clip.length + .01f);
            }
        }

        if (GameObject.FindGameObjectsWithTag("Music").Length > 1)
            Destroy(gameObject);
        
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != level)
            Destroy(this.gameObject);
    }

    void switchtrack()
    {
        b.Play();
    }
}
