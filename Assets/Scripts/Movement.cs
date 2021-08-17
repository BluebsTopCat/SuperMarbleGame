using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public bool penaltyondeath;
    public Timer timer;
    public SpriteRenderer playersprite;
    public BoxCollider2D cube;
    public CircleCollider2D ball;
    public SkinCache skins;
    public AudioSource deathnoise;
    public GameObject deathparticle;
    public bool playerfrozen;
    public GameObject respawpoint;
    public Rigidbody2D circle;
    public float movementspeed = 5f;
    public float acceleration = 5f;
    public float stopacceleration = 7.5f;
    public bool moving;
    public float currentmovespeed;
    public int leftright;
    public float jumpheight;
    public bool touchingground;

    public LineRenderer hook;

    public DistanceJoint2D distancejoint;
    // Update is called once per frame

    public bool hookshotting;
    public float hookshotlength = 20;
    public TrailRenderer trail;

    public GameObject cursor;
    public Texture defaultcursor;
    public Texture greencursor;
    private float cachedposx;
    private Camera maincamera;
    private Vector3 mousepos;
    private RawImage _rawImage;
    public float coyotetime;
    public float currentcoyotetime;
    public GameObject hookpoint;
    public float watergravity;
    public float regulargravity;
    public bool inwater;
    public bool jumpedinwater;
    public bool watermusicplay;
    public float timeoutofwater;
    public float timeinwater;
    private IEnumerator Start()
    {
        _rawImage = cursor.GetComponent<RawImage>();
        distancejoint.enabled = false;
        maincamera = Camera.main;
        yield return new WaitForSeconds(.1f);
        var skin = PlayerPrefs.GetInt("SKIN");
        playersprite.sprite = skins.list[skin].image;
        if (skin == 8)
        {
            cube.enabled = true;
            ball.enabled = false;
        }
        else
        {
            cube.enabled = false;
            ball.enabled = true;
        }
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (playerfrozen)
            return;
        

        if (transform.position.y < -100) StartCoroutine(killplayer());

        if (inwater)
        {
            timeoutofwater = 0;
            timeinwater += Time.deltaTime;
        }
        else
        {
            timeoutofwater += Time.deltaTime;
            timeinwater = 0;
        }

        if (isgrounded())
            currentcoyotetime = 0;
        else
            currentcoyotetime += Time.deltaTime;
        
        trail.gameObject.SetActive(Mathf.Abs(circle.velocity.x) > 10);

        var mouseheld = Input.GetMouseButton(0);
        if (!mouseheld)
        {
            hookshotting = false;
            distancejoint.enabled = false;
            hookpoint.transform.parent = null;
        }
        else
        {
            distancejoint.connectedAnchor = hookpoint.transform.position;
            hook.SetPosition(0, hookpoint.transform.position);
        }

        mousepos = new Vector3(maincamera.ScreenToWorldPoint(Input.mousePosition).x,
            maincamera.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        var correctedmousepos = mousepos;
        cursor.transform.position = maincamera.WorldToScreenPoint(correctedmousepos);

        _rawImage.texture = defaultcursor;

     

        if (Input.GetMouseButtonDown(0))
        {
            if (!canhookthere(correctedmousepos))
                return;
            
            var hit = Physics2D.BoxCast(new Vector2(correctedmousepos.x, correctedmousepos.y), Vector2.one * 1.5f, 0, Vector3.zero);
            if (hit.collider.gameObject.CompareTag("Moving"))
            { 
                hookpoint.transform.parent = hit.transform;
            }
            else
            {
                hookpoint.transform.parent = null;
            }
            hookshotting = true;
            hookpoint.transform.position = correctedmousepos;
            distancejoint.connectedAnchor = hookpoint.transform.position;
            distancejoint.distance = Vector3.Distance(this.transform.position, correctedmousepos);
            hook.SetPosition(0, hookpoint.transform.position);
            distancejoint.enabled = true;
         
        }

        //While you're in a swing;
        if (hookshotting)
        {
            hook.SetPosition(1, transform.position);
            _rawImage.texture = greencursor;
        }
        else
            distancejoint.enabled = false;

        hook.enabled = hookshotting;

        leftright = (int) Input.GetAxisRaw("Horizontal");
        moving = leftright != 0;
        touchingground = isgrounded();

        //Makes it so you can still jump if you've just left the ground
        var force = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.Space) && (currentcoyotetime < coyotetime || (timeoutofwater < .1f && !jumpedinwater)))
        {
       
            force += Vector2.up * jumpheight;
            currentcoyotetime += 1;
            if (inwater)
                jumpedinwater = true;
        }

        //Allows you to change acceleration depending if you're stopping/reversing
        if (moving && !hookshotting && isgrounded())
        {
            if (leftright != currentmovespeed / Mathf.Abs(currentmovespeed))
                force += new Vector2(stopacceleration * leftright, 0) * (Time.deltaTime * 50);
            else
                force += new Vector2(acceleration * leftright, 0) * (Time.deltaTime * 50);
        }
        else
        {
            force += new Vector2(acceleration * leftright / 1.5f, 0) * (Time.deltaTime * 50);
        }
        circle.AddForce(force);
        if (Mathf.Abs(circle.velocity.x) > movementspeed)
            circle.velocity =
                new Vector2(
                    Mathf.Lerp(circle.velocity.x, movementspeed * circle.velocity.x / Mathf.Abs(circle.velocity.x),
                        10f * Time.deltaTime), circle.velocity.y);
    }

    private void OnDrawGizmos()
    {
        if (Vector3.Distance(mousepos, transform.position) < hookshotlength)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(mousepos, 0.5f);
        Gizmos.color = Color.white;
        //Gizmos.DrawWireSphere(this.transform.position, hookshotlength);
        Gizmos.DrawRay(transform.position, transform.right * 1);
    }

    private bool isgrounded()
    {
        //check if a short raycast hits something to see if grounded
        var position = transform.position;

        Debug.DrawLine(position + Vector3.down * .5f, position + Vector3.down * .6f);
        var hit = Physics2D.BoxCastAll(position + Vector3.down * .5f, new Vector2(.05f, 1), 0f, Vector2.down, .05f);
        return hit.Length > 1;
    }

    //gets position of mouse and figures out what the hookshot would hit


    public bool canhookthere(Vector3 pos)
    {
        var transform1 = transform.position;
        var d = mousepos - transform1;
        var hit = Physics2D.BoxCast(new Vector2(pos.x, pos.y), Vector2.one * 1.5f, 0, Vector3.zero);
        if (Physics2D.BoxCast(new Vector2(pos.x, pos.y), Vector2.one, 0, Vector3.zero) && !hit.collider.gameObject.CompareTag("CantHook"))
            return true;
        
        return false;
    }

    public void externalpk()
    {
        if (!playerfrozen)
            StartCoroutine(killplayer());
    }

    public IEnumerator killplayer()
    {
        var thingy = Instantiate(deathparticle, transform);
        playerfrozen = true;
        distancejoint.enabled = false;
        hookshotting = false;
        deathnoise.volume = PlayerPrefs.GetFloat("SFX");
        deathnoise.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        transform.position = respawpoint.transform.position;
        Destroy(thingy);
        GetComponent<SpriteRenderer>().enabled = true;
        playerfrozen = false;
        moving = true;
        circle.velocity = Vector2.zero;
        if (penaltyondeath)
            timer.timer += 5f;
    }
}