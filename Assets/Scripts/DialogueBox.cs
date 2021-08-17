using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public GameObject overlay;
    public float offsety;
    void Update()
    {
        Vector3 trackerpoint = Camera.main.WorldToScreenPoint(overlay.transform.position);
        this.transform.position = new Vector3(trackerpoint.x, trackerpoint.y + offsety, trackerpoint.z);
    }
}
