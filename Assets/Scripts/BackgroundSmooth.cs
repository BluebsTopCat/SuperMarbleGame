using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSmooth : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var transform1 = Camera.main.transform.position + new Vector3(0,2.92f,20);
        this.transform.position = transform1;
        var position = transform1;
        position = new Vector3(Mathf.Floor(position.x*16)/16, Mathf.Floor(position.y * 16)/16,20);
        this.transform.position = position;
    }
}
