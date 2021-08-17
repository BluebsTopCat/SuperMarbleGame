using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStarter : MonoBehaviour
{
    public GameObject bossobject;

    public GameObject gate;

    public GameObject bossanim;

    public AnimationClip startupanim;

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        gate.SetActive(true);
        GameObject.Find("Gates").GetComponent<Animator>().Play("SlamShut");
        bossanim.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(startupanim.length);
        bossanim.SetActive(false);
        bossobject.SetActive(true);
        this.enabled = false;
    }
}
