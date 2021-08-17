using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterChildScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Movement>())
            other.gameObject.GetComponent<Movement>().watermusicplay = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Movement>())
            other.gameObject.GetComponent<Movement>().watermusicplay = false;
    }
}
