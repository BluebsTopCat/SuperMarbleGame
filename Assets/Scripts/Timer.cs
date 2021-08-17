using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Timer : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI text;
    public float bronze;
    public float silver;
    public float gold;
    public Sprite[] medals;
    public Image placement;

    public GameObject canvas;
    public GameObject disablecanvas;
    public Image medal2;
    public TextMeshProUGUI time;
    public TextMeshProUGUI fastesttime;
    public int level;
    public int finaltime;
    private bool completed;
    public HighScores hs;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            if(Input.GetKeyDown(KeyCode.R))
                restartlevel();
            timer += Time.deltaTime;
        }

        string pos1 =((int) timer / 60).ToString();
        while (pos1.Length < 2)
            pos1 = '0' + pos1;
        
        string pos2 =((int)timer%60).ToString();
        while (pos2.Length < 2)
            pos2 = '0' + pos2;
        
        string pos3 =((int)(timer * 1000)%1000).ToString();
        while (pos3.Length < 3)
            pos3 = '0' + pos3;


        string timeText = pos1 + ":" + pos2 + ":" + pos3;
        text.text = timeText;

        if (timer > bronze)
            placement.sprite = medals[0];
        else if (timer > silver)
            placement.sprite = medals[1];
        else if (timer> gold)
            placement.sprite = medals[2];
        else
            placement.sprite = medals[3];
    }

    public void complete()
    {
        canvas.SetActive(true);
        Cursor.visible = true;
        disablecanvas.SetActive(false);
        GameObject.Find("Player").SetActive(false);
        completed = true;
        float timept1 = timer * 1000;
        finaltime = (int) timept1;
        hs.DownloadScores();
        time.text = "Your Time: " + string.Format("{0:D2}:{1:D2}:{2:D2}", 
            (int) finaltime / 60000,
            (int) (finaltime / 1000 % 60),
            (int) finaltime % 1000);

        if (PlayerPrefs.GetFloat("ScoreLevel" + level) > finaltime || PlayerPrefs.GetFloat("ScoreLevel" + level) == 0)
        {
            PlayerPrefs.SetFloat("ScoreLevel" + level, finaltime);
            Debug.Log("Setting ScoreLevel" + level +" to " + finaltime);
            fastesttime.color = Color.red;
        }

        float fastesttimefloat = PlayerPrefs.GetFloat("ScoreLevel" + level);
        Debug.Log(finaltime);    
        Debug.Log(fastesttimefloat);
        fastesttime.text = "High Score: " + string.Format("{0:D2}:{1:D2}:{2:D2}", 
            (int) fastesttimefloat / 60000,
            (int) (fastesttimefloat / 1000 % 60),
            (int) fastesttimefloat % 1000);

        int oldmedal = PlayerPrefs.GetInt("Medal" + level);
        if (timer> bronze)
        {
            medal2.sprite = medals[0];
        }
        else if (timer> silver)
        {
            if(oldmedal < 1)
                PlayerPrefs.SetInt("Medal" + level, 1);
            medal2.sprite = medals[1];
        }
        else if (timer> gold)
        {
            if(oldmedal < 2)
                PlayerPrefs.SetInt("Medal" + level, 2);
            medal2.sprite = medals[2];
        }
        else
        {
            PlayerPrefs.SetInt("Medal" + level, 3);
            medal2.sprite = medals[3];
        }
    }

    public void restartlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}