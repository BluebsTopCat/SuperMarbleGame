using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public void resetgame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("SFX", 0.5f);
        PlayerPrefs.SetFloat("MUSIC", 0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
