using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    private GameObject player;
    private Movement mv;
    private Vector3 desiredpos;
    public Vector4[] cambounds;
    public Vector2 camxbounds;
    public float cameramovespeed;
    private float shakeDuration;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        desiredpos = new Vector3(Mathf.Clamp(player.transform.position.x, camxbounds.x, camxbounds.y), Mathf.Clamp(player.transform.position.y, -2, .36f), -10f);
        mv = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shakeDuration = Mathf.Clamp(shakeDuration - Time.deltaTime, 0, 2);
        float cammin = 0;
        float cammax = 0;
        for (int i = 0; i < cambounds.Length; i++)
        {
            if (this.transform.position.x < cambounds[i].y && this.transform.position.x > cambounds[i].x )
            {
                cammin = cambounds[i].z;
                cammax = cambounds[i].w;
            }
        }

     
        desiredpos = player.transform.position + new Vector3(Mathf.Clamp(3f * mv.leftright, camxbounds.x, camxbounds.y), 1f, -10f);
            
        this.transform.position = Vector3.SmoothDamp(this.transform.position, desiredpos, ref velocity, cameramovespeed);
        
    }
    public void TriggerShake(float shaketime)
    {
        shakeDuration = shaketime;
    }
}
