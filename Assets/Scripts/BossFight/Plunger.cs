using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    public Transform spawnpt;
    public GameObject box;
    public int leftright = 1;
    public bool pressed;
    public SpriteRenderer trigger;
    public Sprite off;
    public Sprite on;
    public GameObject path;
    public GameObject fusebombspawnpoint;
    public bool reload = true;
    public GameObject objtodestroy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pressed)
        {
            if(other.gameObject.name == "Player" && reload)
                 other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-250 * leftright,500,0));
            return;
        }
        Debug.Log("Player Entered!");
        if (other.gameObject.name == "Player")
        {
            GameObject block = Instantiate(box);
            block.transform.position = spawnpt.position;
            block.SetActive(true);
            block.transform.parent = path.transform;
            Fuse f = block.GetComponent<Fuse>();
            f.boomerspawnpos = fusebombspawnpoint;
            f.linetofollow = spawnpt.gameObject.GetComponent<LineRenderer>();
            f.breakthing = !reload;
            f.objecttobreak = objtodestroy;
            if(reload)
                 other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-250 * leftright,500,0));
            StartCoroutine(Cooldown());
            
        }
    }

    IEnumerator Cooldown()
    {
        trigger.sprite = off;
        pressed = true;
        yield return new WaitForSeconds(5);
        if(reload)
        {
            pressed = false;
        trigger.sprite = on;
        }
    }
}
