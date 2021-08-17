using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSwitchScenerey : MonoBehaviour
{
    public Sprite[] left;
    public Color[] leftc;
    public Sprite[] right;
    public Color[] rightc;
    public float xcoord;
    public SpriteRenderer[] bgs;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > xcoord)
        {
            for(int i = 0; i < bgs.Length; i++)
            {
                bgs[i].sprite = right[i];
                bgs[i].color = rightc[i];
            }
        }
        else
        {
            for(int i = 0; i < bgs.Length; i++)
            {
                bgs[i].sprite = left[i];
                bgs[i].color = leftc[i];
            } 
        }
    }
}
