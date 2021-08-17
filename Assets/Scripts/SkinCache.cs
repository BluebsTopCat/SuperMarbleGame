using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class SkinCache : MonoBehaviour
{
    [SerializeField] 
    public bool interactable = true;
    public List<Skin> list = new List<Skin>();
    public int currentskin;
    public Image ball;
    public SpriteRenderer ball2;
    public TextMeshProUGUI name;
    public TextMeshProUGUI deets;
    public string filepath = "";
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        filepath = PlayerPrefs.GetString("DIR");
        currentskin = PlayerPrefs.GetInt("SKIN", 0);
      if (filepath != "")
        {
            using (WebClient webClient = new WebClient()) 
            {
                webClient.DownloadFile(filepath,  @"C:\tmp\playermarble.png") ; 
            }
            Sprite s = Sprite.Create(LoadPNG(@"C:\tmp\playermarble.png"), new Rect(0,0,LoadPNG(@"C:\tmp\playermarble.png").width,LoadPNG(@"C:\tmp\playermarble.png").width), Vector2.one/2, LoadPNG(@"C:\tmp\playermarble.png").width);
            list[19].image = s;
        }
    }

    void Update()
    {
        if (interactable)
        {
        if (currentskin >= list.Count)
            currentskin -= list.Count;
        if (currentskin < 0)
            currentskin = list.Count -1;
  
            if(ball != null)
                 ball.sprite = list[currentskin].image;
            if(ball2 != null)
                ball2.sprite = list[currentskin].image;
              
            name.text = list[currentskin].name;
            deets.text = list[currentskin].details;
            PlayerPrefs.SetInt("SKIN", currentskin);
        }
    }

    public void left()
    {
        currentskin--;
    }

    public void right()
    {
        currentskin++;
    }
    //Cut in Browser
    
    public static Texture2D LoadPNG(string filePath) {
 
        Texture2D tex = null;
        byte[] fileData;
 
        if (File.Exists(filePath))     {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(1, 1);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            Debug.Log("Found!");
            //tex.filterMode = FilterMode.Point;
        }
        return tex;
    }
    
  public void newdir(GameObject g)
    {
        string str = g.GetComponent<TMP_InputField>().text;
        filepath = str;
        PlayerPrefs.SetString("DIR",str);
        if (filepath != null)
        {
            using (WebClient webClient = new WebClient()) 
            {
                webClient.DownloadFile(filepath,  @"C:\tmp\playermarble.png") ; 
            }
            Sprite s = Sprite.Create(LoadPNG(@"C:temp\playermarble.png"), new Rect(0,0,LoadPNG(@"C:\tmp\playermarble.png").width,LoadPNG(@"C:\tmp\playermarble.png").width), Vector2.one/2, LoadPNG(@"C:\tmp\playermarble.png").width);
            list[19].image = s;
        }

    }

}
[System.Serializable]
public class Skin {
    public string name;
    public Sprite image;
    public string details;

    public Skin(string nm, Sprite img, string det)
    {
        name = nm;
        image = img;
        details = det;
    }
}

