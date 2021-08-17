using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public Vector2 rbspeed;

    public AudioSource audiosrc; 
    private GameObject player;
    

    private Rigidbody2D prb;
    
    public float initialAngle = 30;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Movement>())
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = calcBallisticVelocityVector(player.transform.position, this.transform.position + Vector3.up * rbspeed.y +Vector3.right * rbspeed.x, initialAngle);
            audiosrc.volume = PlayerPrefs.GetFloat("SFX") * .5f;
            audiosrc.Play();
        }
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        prb = player.GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.up * rbspeed.y +Vector3.right * rbspeed.x);
            Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( this.transform.position + Vector3.up * rbspeed.y +Vector3.right * rbspeed.x, .5f);
        this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(rbspeed.normalized.y, rbspeed.normalized.x) * Mathf.Rad2Deg +  initialAngle);
        
    }

    Vector2 calcBallisticVelocityVector(Vector2 source, Vector2 target, float angle){
        Vector3 direction = target - source;                            
        float h = direction.y;                                           
        direction.y = 0;                                               
        float distance = direction.magnitude;                           
        float a = (angle + 1) * Mathf.Deg2Rad;                                
        direction.y = distance * Mathf.Tan(a);                            
        distance += h/Mathf.Tan(a);                                      
 
        // calculate velocity
        float velocity = Mathf.Sqrt(distance * (Physics.gravity.magnitude * prb.gravityScale)/ Mathf.Sin(2*a));
        return velocity * direction.normalized;    
    }


}
