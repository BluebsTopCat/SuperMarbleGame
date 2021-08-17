using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour 
{
    public TMPro.TextMeshProUGUI[] rNames;
    public TMPro.TextMeshProUGUI[] rScores;
    HighScores myScores;

    void Awake() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = "___";
            rScores[i].text = "XX:XX:XXX";
        }
        myScores = GetComponent<HighScores>();
    }
    public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = "";
            if (highscoreList.Length > i)
            {
                highscoreList[i].score = 1000000000 - highscoreList[i].score;
                rScores[i].text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                    highscoreList[i].score / 60000,
                    highscoreList[i].score/ 1000 % 60,
                    highscoreList[i].score % 1000);

                rNames[i].text = highscoreList[i].username;
            }
        }
    }

}
