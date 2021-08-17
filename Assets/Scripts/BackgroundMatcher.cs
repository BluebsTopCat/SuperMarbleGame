using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMatcher : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
       this.transform.localScale = new Vector3(1, (float)Screen.height/1080,0f); 
       Debug.Log(Screen.width + " " + Screen.height);
    }
}
