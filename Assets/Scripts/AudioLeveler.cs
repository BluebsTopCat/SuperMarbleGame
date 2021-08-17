using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLeveler : MonoBehaviour
{
    public AudioSource a;

    // Update is called once per frame
    void Update()
    {
        a.volume = PlayerPrefs.GetFloat("SFX");
    }
}
