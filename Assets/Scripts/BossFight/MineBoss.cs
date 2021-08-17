using Unity.Mathematics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MineBoss : MonoBehaviour
{
    public GameObject Lefthand;
    public Animator leftanim;
    public Transform lefthandhome;

    public GameObject Righthand;
    public Animator rightanim;
    public Transform righthandhome;

    public float timebetweenattacks = 5f;
    public float followbeforesmashtime = 1f;

    public float timesincelastattack;
    private static readonly int Smash = Animator.StringToHash("Smash");

    public bool lefthandfollowing;
    public float ltimefollowing;
    public bool righthandfollowing;
    public float rtimefollowing;
    public bool rhandattacking;
    public bool lhandattacking;
    private GameObject player;

    public SpriteRenderer face;
    public Sprite normal;
    public Sprite mad;
    public Sprite brokenormal;
    public Sprite brokemad;
    public Sprite hurtsprite;
    public Sprite fakeouthurt;
    public Sprite spritedead;

    public bool usinglefthand = false;

    public GameObject spikes;

    public GameObject spikespawnpos;
    // Start is called before the first frame update
    public int hp = 4;

    void Start()
    {
        this.GetComponent<Animator>().Play("FuckinPissed");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0)
            die();
        
        if (timesincelastattack > timebetweenattacks &&
            !(lefthandfollowing || righthandfollowing || lhandattacking || rhandattacking))
        {
            usinglefthand = !usinglefthand;
            timesincelastattack = 0;
            if (usinglefthand)
            {
                if (UnityEngine.Random.Range(0, 2) == 0 && player.transform.position.y < this.transform.position.y)
                {
                    lefthandfollowing = true;
                }
                else
                {
                    Lefthand.transform.position = lefthandhome.transform.position;
                    StartCoroutine(stalactites());
                }
            }
            else
            {
                if (UnityEngine.Random.Range(0, 2) == 0 && player.transform.position.y < this.transform.position.y)
                {
                    righthandfollowing = true;
                }
                else
                {
                    Righthand.transform.position = righthandhome.transform.position;
                    StartCoroutine(sweep("sweep"));
                }
            }
        }
        else if (!(lefthandfollowing || righthandfollowing || lhandattacking || rhandattacking))
            timesincelastattack += Time.deltaTime;

        if (lefthandfollowing)
        {
            ltimefollowing += Time.deltaTime;
            var position = Lefthand.transform.position;
            position = Vector3.MoveTowards(position,
                new Vector3(player.transform.position.x, position.y, position.z),
                10 * Time.deltaTime);
            Lefthand.transform.position = position;
        }
        else if (!lhandattacking)
        {
            var position = Lefthand.transform.position;
            position = Vector3.MoveTowards(position,
                lefthandhome.position,
                10 * Time.deltaTime);
            Lefthand.transform.position = position;
        }

        if (righthandfollowing)
        {
            rtimefollowing += Time.deltaTime;
            var position = Righthand.transform.position;
            position = Vector3.MoveTowards(position,
                new Vector3(player.transform.position.x, position.y, position.z),
                10 * Time.deltaTime);
            Righthand.transform.position = position;
        }
        else if (!rhandattacking)
        {
            var position = Righthand.transform.position;
            position = Vector3.MoveTowards(position,
                righthandhome.position,
                10 * Time.deltaTime);
            Righthand.transform.position = position;
        }

        if (rtimefollowing > followbeforesmashtime)
        {
            rtimefollowing = 0;
            righthandfollowing = false;
            StartCoroutine(attack(rightanim, "Smash", false));
        }

        if (ltimefollowing > followbeforesmashtime)
        {
            ltimefollowing = 0;
            lefthandfollowing = false;
            StartCoroutine(attack(leftanim, "Smash", true));
        }
    }

    public IEnumerator attack(Animator anim, string name, bool left)
    {
        if (hp > 2)
            face.sprite = mad;
        else
            face.sprite = brokemad;
        if (left)
            lhandattacking = true;
        else
            rhandattacking = true;
        anim.Play(name);
        yield return new WaitForSeconds(2f);
        if (left)
            lhandattacking = false;
        else
            rhandattacking = false;
        if (hp > 2)
            face.sprite = normal;
        else
            face.sprite = brokenormal;
    }

    public IEnumerator sweep(string name)
    {
        if (hp > 2)
            face.sprite = mad;
        else
            face.sprite = brokemad;
        
        rhandattacking = true;
        
        rightanim.Play(name);
        yield return new WaitForSeconds(1.2f);
        Righthand.transform.position = new Vector3(Righthand.transform.position.x,player.transform.position.y,Righthand.transform.position.z);
        yield return new WaitForSeconds(1.3f);
        Righthand.transform.position = new Vector3(Righthand.transform.position.x,righthandhome.transform.position.y,Righthand.transform.position.z);
        rhandattacking = false;
        
        if (hp > 2)
            face.sprite = normal;
        else
            face.sprite = brokenormal;
    }

    public IEnumerator stalactites()
    {
        if (hp > 2)
            face.sprite = mad;
        else
            face.sprite = brokemad;

        lhandattacking = true;
        leftanim.Play("Stalactites");
        yield return new WaitForSeconds(1f);
        GameObject fall = Instantiate(spikes,spikespawnpos.transform);
        fall.transform.position = spikespawnpos.transform.position;
        yield return new WaitForSeconds(.5f);
        lhandattacking = false;
        
        if (hp > 2)
            face.sprite = normal;
        else
            face.sprite = brokenormal;
    }
    public void hurt()
    {
        StartCoroutine(hurti());
    }

    public IEnumerator hurti()
    {
        hp--;
        if (hp <= 0)
        {
            die();
        }
        else
        {
            face.sprite = hurtsprite;
            for (int i = 0; i < 3; i++)
            {
                face.color = Color.red;
                yield return new WaitForSeconds(.1f);
                face.color = Color.white;
                yield return new WaitForSeconds(.1f);
            }

            if (hp <= 2)
                face.sprite = brokenormal;
            else
                face.sprite = normal;
        }
    }

    public void die()
    {
        face.sprite = spritedead;
        leftanim.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        leftanim.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        leftanim.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
        for (int i = 0; i < leftanim.gameObject.transform.childCount; i++)
        {
            Destroy(leftanim.gameObject.transform.GetChild(i).gameObject);
        }

        rightanim.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        rightanim.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rightanim.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
        for (int i = 0; i < rightanim.gameObject.transform.childCount; i++)
        {
            Destroy(rightanim.gameObject.transform.GetChild(i).gameObject);
        }

        Destroy(leftanim);
        Destroy(rightanim);
        Destroy(Lefthand.GetComponent<Animator>());
        Destroy(Righthand.GetComponent<Animator>());
        GameObject.Find("Gates").GetComponent<Animator>().Play("gateopen");
        Destroy(GameObject.Find("Gates"));
        Destroy(this.gameObject.GetComponent<MineBoss>());
    }

    public void donk()
    {
        StartCoroutine(donki());
    }

    public IEnumerator donki()
    {
        face.sprite = fakeouthurt;
        yield return new WaitForSeconds(.3f);
        
        if (hp <= 2)
            face.sprite = brokenormal;
        else
            face.sprite = normal;
    }
}