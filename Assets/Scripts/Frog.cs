using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public Vector2 timebetweenjumps;
    public bool jumping;

    public Sprite frogground;

    public Sprite frogair;

    public SpriteRenderer frog;

    public Rigidbody2D frogebody;

    private float facing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(facing, 1f,1f);
        if (!jumping && Physics2D.Raycast(this.transform.position + Vector3.down * .5f, Vector3.down, .1f))
        {
            jumping = true;
            StartCoroutine(Jump());
        }

        if(Physics2D.Raycast(this.transform.position + Vector3.down * .5f, Vector3.down, .1f))
        {
            frog.sprite = frogground;
        }
        else
        {
            frog.sprite = frogair;
        }
    }

    IEnumerator Jump()
    {

        if (Random.value > .5)
            facing = -1;
        else
            facing = 1;
        frogebody.AddForce(new Vector2(Random.Range(0, 250) * -facing, Random.Range(150, 300)));
        yield return new WaitForSeconds(Random.Range(timebetweenjumps.x, timebetweenjumps.y));
        jumping = false;
    }
}
