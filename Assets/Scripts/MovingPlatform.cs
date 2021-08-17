using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private SpriteRenderer parent;
    private float size;
    public bool on;
    public float speed;
    public bool horizontal;

    public bool loop = false;

    public float offset = 0f;
    public int flip = 1;

    private float toffset;
    // Start is called before the first frame update
    void Start()
    {
        toffset = Time.time;
        parent = this.transform.parent.GetComponent<SpriteRenderer>();
        size = parent.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        float timeadjusted = (Time.time - toffset);
        if (on)
        {
            if(!loop)
             this.transform.localPosition = new Vector3(flip * (Mathf.Abs(((timeadjusted * 2 * speed / size + offset) % 4) - 2) * size - size), 0f, 0f);
            else
            {
                this.transform.localPosition = new Vector3(flip * ((timeadjusted * speed + offset) % size * 2 - size), 0f, 0f);
            }
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (horizontal)
        {
            collision.transform.SetParent(this.transform, true);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null, true);
    }
    

}
