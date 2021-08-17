using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ArmIK : MonoBehaviour
{
    public float length;

    public LineRenderer line;

    public GameObject start;

    public GameObject end;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        Vector3 spos = start.transform.position;
        Vector3 epos = end.transform.position;
        Vector3 center = CenterX(spos.x, spos.y, epos.x, epos.y, length);
        
        line.positionCount = 3;
        line.SetPosition(0, start.transform.position);
        line.SetPosition(1, center);
        line.SetPosition(2, end.transform.position);
    }
    private Vector3 CenterX(float x1,float y1, float x2, float y2,float radius)
    {
        float radsq = radius * radius;
        float q = Mathf.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
        float x3 = (x1 + x2) / 2;
        float y3 = (y1 + y2) / 2;
        
        
        return new Vector3(x3+ Mathf.Sqrt(radsq - ((q / 2) * (q / 2))) * ((y1 - y2) / q), y3+ Mathf.Sqrt(radsq - ((q / 2) * (q / 2))) * ((x2-x1) / q), 0f);


    }

}
