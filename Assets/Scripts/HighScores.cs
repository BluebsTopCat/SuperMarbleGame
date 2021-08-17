using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    private List<LeaderboardPair> pairs = new List<LeaderboardPair>();
    public int level;
    const string webURL = "http://dreamlo.com/lb/"; //  Website the keys are for

    public PlayerScore[] scoreList;
    DisplayHighscores myDisplay;

    static HighScores instance; //Required for STATIC usability
    void Awake()
    {
        instance = this; //Sets Static Instance
        myDisplay = GetComponent<DisplayHighscores>();
        pairs.Clear();
        pairs.Add(new LeaderboardPair("4o5GqWALDEiawLN2vgkhEwkaiptnVNWUWFCuNKPzfKAA", "60a3f7938f40bb71f0c87925")); //level 1
        pairs.Add(new LeaderboardPair("1klXoVSgk0WT2mO-6_f_GwYt7HEWdXXkK4OrgVVKm1aQ", "60a3f7eb8f40bb71f0c879a8")); //level 2
        pairs.Add(new LeaderboardPair("OxA4bMU6rE-mPe13RV9v_ghynnw_QT_EiEzMtWk3sRVQ", "609f2c0c8f40bb71f0c2cfab")); //level 3
        pairs.Add(new LeaderboardPair("Egp8Z_VBkU-AaGJyAfg5-wFeB43KtGaEObOMXwLvjs_A", "60a3f94f8f40bb71f0c87b95")); //level 4
        
        pairs.Add(new LeaderboardPair("0OKcC-rtSkqODPufbqXShQXDosZxO5TkaH0Cz401L4lw", "60a3f97b8f40bb71f0c87bda")); //World 2 Level 1
    }
    
    public void UploadScore(string username, int score)  //CALLED when Uploading new Score to WEBSITE
    {//STATIC to call from other scripts easily
        instance.StartCoroutine(instance.DatabaseUpload(username,score)); //Calls Instance
    }

    IEnumerator DatabaseUpload(string userame, int score) //Called when sending new score to Website
    {
        int uploadscore = 1000000000 - score;
        WWW www = new WWW(webURL + pairs[level-1].PrivKey + "/add/" + WWW.EscapeURL(userame) + "/" +uploadscore);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadScores();
        }
        else print("Error uploading" + www.error);
    }

    public void DownloadScores()
    {
        StartCoroutine("DatabaseDownload");
    }
    IEnumerator DatabaseDownload()
    {
        //WWW www = new WWW(webURL + publicCode + "/pipe/"); //Gets the whole list
        WWW www = new WWW(webURL + pairs[level-1].PubKey + "/pipe/0/4"); //Gets top 10
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            OrganizeInfo(www.text);
            myDisplay.SetScoresToMenu(scoreList);
        }
        else print("Error uploading" + www.error);
    }

    void OrganizeInfo(string rawData) //Divides Scoreboard info by new lines
    {
        string[] entries = rawData.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        scoreList = new PlayerScore[entries.Length];
        for (int i = 0; i < entries.Length; i ++) //For each entry in the string array
        {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]); //Error Here
            scoreList[i] = new PlayerScore(username,score);
            print(scoreList[i].username + ": " + scoreList[i].score);
        }
    }
}

public struct PlayerScore //Creates place to store the variables for the name and score of each player
{
    public string username;
    public int score;

    public PlayerScore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}

public class LeaderboardPair {
    public string PrivKey;
    public string PubKey;

    public LeaderboardPair(string priv, string pub)
    {
        PrivKey = priv;
        PubKey = pub;
    }
}