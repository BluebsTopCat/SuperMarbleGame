using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliderthing : MonoBehaviour
{
    public bool music;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        if (music)
            slider.value = PlayerPrefs.GetFloat("MUSIC");
        else
            slider.value = PlayerPrefs.GetFloat("SFX");
    }

}
