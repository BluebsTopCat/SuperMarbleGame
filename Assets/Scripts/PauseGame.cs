using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject ui;
    public bool ingame = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ingame = !ingame;
        
        if (ingame)
        {
            Time.timeScale = 1;
            ui.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            ui.SetActive(true);
        }
    }

    public void quittomenu()
    {
   
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ingame = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
