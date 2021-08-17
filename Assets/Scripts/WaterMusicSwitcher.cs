using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMusicSwitcher : MonoBehaviour
{
    public AudioSource abovewater;

    public AudioSource belowwater;

    public Movement player;

    private float musicvolume;

    private float nothing = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        musicvolume = PlayerPrefs.GetFloat("MUSIC");
        abovewater.volume = musicvolume;
        belowwater.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindObjectOfType<Movement>();
        if (player.watermusicplay)
        {
            abovewater.volume = Mathf.SmoothDamp(abovewater.volume, 0f, ref nothing, .025f);
            belowwater.volume = Mathf.SmoothDamp(belowwater.volume, musicvolume, ref nothing, .025f);
        }
        else
        {
            abovewater.volume = Mathf.SmoothDamp(abovewater.volume, musicvolume, ref nothing, .025f);
            belowwater.volume = Mathf.SmoothDamp(belowwater.volume, 0f, ref nothing, .025f);
        }
    }
}
