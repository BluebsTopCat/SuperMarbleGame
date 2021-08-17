using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RotationalMenu : MonoBehaviour
{
    public Animator anim;
    private static readonly int Active = Animator.StringToHash("State");
    public int activescene = 0;

    private void Update()
    {
       if(activescene == 0 && Input.GetKeyDown(KeyCode.Space)) 
           Setactivescreen(1);
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    public void fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Setactivescreen(int i)
    {
        anim.SetInteger(Active, i);
        activescene = i;
    }

    public void Setmusic(GameObject s)
    {
        PlayerPrefs.SetFloat("MUSIC", s.GetComponent<Slider>().value);
    }
    public void Setsfx(GameObject s)
    {
        PlayerPrefs.SetFloat("SFX", s.GetComponent<Slider>().value);
    }

    public void Loadlevel(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Vsync(GameObject g)
    {
        bool on = g.GetComponent<Toggle>().isOn;
        if (on)
        {
            QualitySettings.vSyncCount = 1;
            GameObject.Find("Color123").GetComponent<Image>().color = Color.green;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            GameObject.Find("Color123").GetComponent<Image>().color = Color.red;
        }
    }
}
