using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliderinit : MonoBehaviour
{
    public Slider slider;

    public bool music;
    // Start is called before the first frame update
    void Start()
    {
        if (music)
        {
            if (PlayerPrefs.GetFloat("MUSIC", 2) == 2)
                slider.value = .5f;
            else
                slider.value = PlayerPrefs.GetFloat("MUSIC");
        }
        else
        {
            if (PlayerPrefs.GetFloat("SFX", 2) == 2)
                slider.value = .5f;
            else
                slider.value = PlayerPrefs.GetFloat("SFX");
        }
    }
    
}
