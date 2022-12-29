using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;

public class Smiting : MonoBehaviour
{
    public Transform tr;
    public Transform me;
    private Rigidbody toThrow;
    bool smeltingnow = false;
    public Transform bin;
    public mining other;
    Vector3 currentSpeed;
    Vector3 binLocation = new Vector3(-163.62f, 0, -23.15f);
    Vector3 bridge = new Vector3(-38.2f, 0, 24.66f);
    Vector3 terrace = new Vector3(-112.93f, 28f, -33.78f);
    bool there = false;
    List <GameObject> toSmelt = new List <GameObject>();
    List <GameObject> smelted = new List <GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = new Vector3((me.transform.position.x - bridge.x) / 2000f, 0, (me.transform.position.z - bridge.z) / 2000f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!there) {
            if (getDistance(me.transform.position, bridge) < 2) {
                print("h");
                currentSpeed = new Vector3((me.transform.position.x - terrace.x) / 6000f, 0, (me.transform.position.z - terrace.z) / 6000f);
            } else if (getDistance(me.transform.position, terrace) < 5) {
                currentSpeed = new Vector3((me.transform.position.x - tr.transform.position.x) / 2000f, 0, (me.transform.position.z - tr.transform.position.z) / 2000f);
            } else if (getDistance(me.transform.position, tr.transform.position) < 6) {
                there = true;
            } 
            StartCoroutine(toTheForge());
        } else {
            if (toSmelt.Count > 0) {
                if (getDistance(me.transform.position, tr.transform.position) < 6 && !smeltingnow) {
                    currentSpeed = new Vector3((me.transform.position.x - binLocation.x) / 2000f, 0, (me.transform.position.z - binLocation.z) / 2000f);
                    StartCoroutine(startSmelting());
                } else if (getDistance(me.transform.position, binLocation) < 5) {
                    me.transform.DetachChildren();
                    bin.transform.DetachChildren();
                    StartCoroutine(startSmelting());
                } else {
                    StartCoroutine(startSmelting());
                }
            } else if (other.childrens.Count >= 10) {
                if (getDistance(me.transform.position, tr.transform.position) < 6) {
                    currentSpeed = new Vector3((me.transform.position.x - bin.transform.position.x) / 500f, 0, (me.transform.position.z - bin.transform.position.z) / 500f);
                } else if (getDistance(me.transform.position, bin.transform.position) < 6 && bin.transform.parent != me.transform) {
                    bin.transform.parent = me.transform;
                    currentSpeed = new Vector3((me.transform.position.x - terrace.x) / 2000f, 0, (me.transform.position.z - terrace.z) / 2000f);
                } else if (getDistance(me.transform.position, terrace) < 1) {
                    currentSpeed = new Vector3((me.transform.position.x - bridge.x) / 2000f, 0, (me.transform.position.z - bridge.z) / 2000f);
                } else if (getDistance(me.transform.position, bridge) < 1) {
                    currentSpeed = new Vector3((me.transform.position.x - other.childrens[0].transform.position.x) / 2000, 0, (me.transform.position.z - other.childrens[0].transform.position.z) / 2000);
                } else if (getDistance(me.transform.position, other.childrens[0].transform.position) < 1) {
                    int i = 0;
                    foreach (GameObject go in other.childrens) {
                        go.transform.position = new Vector3 (bin.transform.position.x + Random.Range(-2, 2), bin.transform.position.y + 2, bin.transform.position.z + Random.Range(-2,2));
                        go.transform.parent = bin.transform;
                        i++;
                    }
                    toSmelt = other.childrens;
                    other.childrens.Clear();
                    currentSpeed = new Vector3((me.transform.position.x - bridge.x) / 2000f, 0, (me.transform.position.z - bridge.z) / 2000f);
                    there = false;
                } 
                StartCoroutine(toTheForge());
            }
            
        }
    }

    IEnumerator toTheForge() {
        yield return new WaitForSeconds(0.01f);
        transform.Translate(-currentSpeed.x, 0, -currentSpeed.z);
    }

    IEnumerator startSmelting() {
        smeltingnow = true;
        Vector3 smeltLocation = new Vector3 (-137.587f, 0, -44.258f);
        yield return new WaitForSeconds(0.01f);
        if (getDistance(me.transform.position, binLocation) < 5) {
            toSmelt[0].transform.position = new Vector3 (me.transform.position.x, me.transform.position.y + 0.5f, me.transform.position.z);
            toSmelt[0].transform.parent = me.transform;
            currentSpeed = new Vector3((me.transform.position.x - smeltLocation.x) / 2000f, 0, (me.transform.position.z - smeltLocation.z) / 2000f);
        } else if (getDistance(me.transform.position, smeltLocation) < 1f) {
            toThrow = toSmelt[0].GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(toSmelt[0].transform.position.x - 144.7f, 0, toSmelt[0].transform.position.z -48.79f);
            me.transform.DetachChildren();
            toThrow.AddForce(direction.x/4, 0.6f, direction.z/4, ForceMode.Impulse);
            smelted.Add(toSmelt[0]);
            toSmelt.RemoveAt(0);
            currentSpeed = new Vector3((me.transform.position.x - binLocation.x) / 2000f, 0, (me.transform.position.z - binLocation.z) / 2000f);
        }
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
