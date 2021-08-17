using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    private Vector3[] path;
    public bool instantiate = false;
    public GameObject boomer;
    public GameObject boomerspawnpos;
    public LineRenderer linetofollow;
    public float speed = .3f;

    private int i = 0;
    private Vector3 velocity = Vector3.zero;

    public bool breakthing;
    public GameObject objecttobreak;
    // Start is called before the first frame update
    void Start()
    {
        path = new Vector3[linetofollow.positionCount];
        for (int x = 0; x < linetofollow.positionCount; x++)
        {
            path[x] = linetofollow.GetPosition(x) + this.transform.parent.transform.position;
        }
        this.transform.position = path[0];
        i = 1;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, path[i],speed*Time.deltaTime);
        
        if (Vector3.Distance(this.transform.position,path[i]) < .1f)
            i++;

        if (i >= path.Length)
        {
            if(!instantiate)
                boomer.SetActive(true);
            else
            {
                GameObject g = Instantiate(boomer);
                g.transform.position = boomerspawnpos.transform.position;
                g.SetActive(true);
                Debug.Log("DidStalactite! " + this.name);
                Destroy(this.gameObject);
            }

            if (breakthing)
                Destroy(objecttobreak);
            gameObject.SetActive(false);
        }
    }
}
