using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tracking : MonoBehaviour
{
    public Transform treegroup;
    public Transform self;
    Vector3 currentDistance;
    Vector3 homePosition;
    Vector3 initialSpeed;
    public int basket = 0;
    public int berries = 0;
    List <int> foods = new List<int>();
    int place = 0;
    float closest;
    public Transform target = null;
    // Start is called before the first frame update
    void Start()
    {
        homePosition = new Vector3(self.transform.position.x, 0, self.transform.position.z);
        currentDistance = new Vector3(self.transform.position.x - treegroup.GetChild(0).transform.position.x, 0, self.transform.position.z - treegroup.GetChild(0).transform.position.z);

        closest = toFloat(Math.Sqrt(Math.Pow(currentDistance.x, 2) + Math.Pow(currentDistance.z, 2)));
        for (int i = 0; i < treegroup.childCount; i++) {
            foods.Add(10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        closest = 1000;
        if (target == null) {
            for (int i=0; i< treegroup.childCount; i++) {
                currentDistance = new Vector3(self.transform.position.x - treegroup.GetChild(i).transform.position.x, 0, self.transform.position.z - treegroup.GetChild(i).transform.position.z);
                if (toFloat(Math.Sqrt(Math.Pow(currentDistance.x, 2) + Math.Pow(currentDistance.z, 2))) < closest && foods[i] > 0) {
                    target = treegroup.GetChild(i);
                    closest = toFloat(Math.Sqrt(Math.Pow(currentDistance.x, 2) + Math.Pow(currentDistance.z, 2)));
                    place = i;
                } 
            } 
            initialSpeed = new Vector3((self.transform.position.x - target.position.x) / 100.0f, 0, (self.transform.position.z - target.position.z) / 100.0f);
        } else {
            print(getDistance(self.transform.position, target.transform.position));
            if (getDistance(self.transform.position, target.transform.position) < 1) {
                foods[place] -= 10;
                basket+=10;
                initialSpeed = new Vector3((self.transform.position.x - homePosition.x) / 100.0f, 0, (self.transform.position.z - homePosition.z) / 100.0f);
            } else if (getDistance(self.transform.position, homePosition) < 1 && basket == 10) {
                basket = 0;
                berries += 10;
                target = null;
            
            }else if (basket == 0) {
                StartCoroutine(Path());
            } else {
                print('h');
                initialSpeed = new Vector3((self.transform.position.x - homePosition.x) / 100.0f, 0, (self.transform.position.z - homePosition.z) / 100.0f);
                StartCoroutine(Path());
            }
        }
    }
    private float toFloat(double a) {
        return (float)a;
    }

    public float getDistance (Vector3 a, Vector3 b) {
        Vector3 placeholder = new Vector3(a.x - b.x, 0, a.z - b.z);
        return(toFloat(Math.Sqrt(Math.Pow(placeholder.x, 2) + Math.Pow(placeholder.z, 2))));
    }
    IEnumerator Path()
    {
        yield return new WaitForSeconds(0.01f);
        transform.Translate(-initialSpeed.x, 0, -initialSpeed.z);
    }
    public Vector3 subtract (Vector3 subtractor, Vector3 subtractee) {
        return(new Vector3(subtractee.x - subtractor.x, 0, subtractee.z - subtractee.z));
    }
}
