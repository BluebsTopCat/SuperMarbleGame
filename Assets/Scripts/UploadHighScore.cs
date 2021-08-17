using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UploadHighScore : MonoBehaviour
{
    public Timer t;

    public TMP_InputField inpt;

    public HighScores hs;
    public Button butn;
        public void sendscore()
    {
        hs.UploadScore(inpt.text, t.finaltime);
        hs.DownloadScores();
        StartCoroutine(freeze());
    }

        public IEnumerator freeze()
        {
            butn.interactable = false;
            yield return new WaitForSeconds(30);
            butn.interactable = true;
        }
}
