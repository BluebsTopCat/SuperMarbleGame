using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FakeWalls : MonoBehaviour
{
   public Tilemap tilemap;
   public void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("Trigger entered");
      if(other.gameObject.name == "Player")
         tilemap.color = new Color(255,255,255, 0);
      Debug.Log(tilemap.color.a);
   }

   public void OnTriggerExit2D(Collider2D other)
   {
      Debug.Log("Trigger exited");
      if(other.gameObject.name == "Player")
         tilemap.color = new Color(255,255,255, 255);
   }
}
