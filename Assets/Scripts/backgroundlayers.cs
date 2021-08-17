using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundlayers : MonoBehaviour
{

    public Material[] layers;
    public float[] speeds;
    public float[] verticalspeeds;
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < layers.Length; i++)
        {
            layers[i].mainTextureOffset = new Vector2(this.transform.position.x*speeds[i] + i * .5f,this.transform.position.y * verticalspeeds[i] );
        }
    }
}
