using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class blacksmithWalk : MonoBehaviour
{
    public Transform tr;
    public Transform me;
    Vector3 currentSpeed;
    Vector3 bridge = new Vector3(-38.2f, 0, 24.66f);
    Vector3 terrace = new Vector3(-112.93f, 28f, -33.78f);
    bool there = false;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = new Vector3((me.transform.position.x - bridge.x) / 2000f, 0, (me.transform.position.z - bridge.z) / 2000f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!there) {
            if (getDistance(me.transform.position, bridge) < 1) {
                currentSpeed = new Vector3((me.transform.position.x - terrace.x) / 6000f, 0, (me.transform.position.z - terrace.z) / 6000f);
            } else if (getDistance(me.transform.position, terrace) < 5) {
                currentSpeed = new Vector3((me.transform.position.x - tr.transform.position.x) / 2000f, 0, (me.transform.position.z - tr.transform.position.z) / 2000f);
            } else if (getDistance(me.transform.position, tr.transform.position) < 1.2f) {
                there = true;
            } 
            StartCoroutine(toTheForge());
        } else {
        }
    }

    IEnumerator toTheForge() {
        yield return new WaitForSeconds(0.01f);
        transform.Translate(-currentSpeed.x, 0, -currentSpeed.z);
    }
    float getDistance (Vector3 a, Vector3 b) {
        Vector3 placeholder = new Vector3(a.x - b.x, 0, a.z - b.z);
        return(toFloat(Math.Sqrt(Math.Pow(placeholder.x, 2) + Math.Pow(placeholder.z, 2))));
    }
    
    private float toFloat(double a) {
        return (float)a;
    }
}
