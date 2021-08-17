using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuIcon : MonoBehaviour
{
    public int level;
    public Image medal;
    public bool lockiflast = false;
    public Button button;
    public Image levelimage;
    public Sprite unlocked;
    public Sprite locked;
    public Sprite[] medals;
    // Update is called once per frame
    void Start()
    {
        medal.sprite = medals[PlayerPrefs.GetInt("Medal" + level)];
        if (lockiflast && PlayerPrefs.GetInt("Medal" + (level - 1)) == 0)
        {
            button.enabled = false;
            levelimage.sprite = locked;
        }
        else
        {
            button.enabled = true;
            levelimage.sprite = unlocked;
        }
    }
}
