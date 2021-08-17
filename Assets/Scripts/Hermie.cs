using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermie : MonoBehaviour
{
    public GameObject player;

    private Rigidbody2D rb;

    private Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody2D>();
        anime = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > this.transform.position.x)
            this.transform.localScale = new Vector3(-1, 1,1);
        else
            this.transform.localScale = Vector3.one;

        if (Vector3.Distance(player.transform.position, this.transform.position) < 5)
        {
            this.rb.transform.Translate(new Vector2(2 * this.transform.localScale.x * Time.deltaTime, 0f));
            anime.enabled = true;
        }
        else
        {
            anime.enabled = false;
        }
    }
}
