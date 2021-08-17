using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{
    public GameObject ui;
    public Vector3 offset;
    private void OnTriggerEnter2D(Collider2D other)
    {
        ui.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) + offset;
        ui.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ui.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position) + offset;
        ui.transform.position = pos;
    }
}
