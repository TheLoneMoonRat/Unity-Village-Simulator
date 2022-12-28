using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tracking : MonoBehaviour
{
    public Transform treegroup;
    public Transform bowl;
    public Transform barrel;
    public Transform self;
    Vector3 currentDistance;
    Vector3 homePosition;
    public instantiator other; 
    Vector3 initialSpeed;
    public int basket = 0;
    public int berries = 0;
    public float movementSpeed = 150.0f;
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
        closest = 100000;
        if (target == null) {
            for (int i=0; i< treegroup.childCount; i++) {
                currentDistance = new Vector3(self.transform.position.x - treegroup.GetChild(i).transform.position.x, 0, self.transform.position.z - treegroup.GetChild(i).transform.position.z);
                if (toFloat(Math.Sqrt(Math.Pow(currentDistance.x, 2) + Math.Pow(currentDistance.z, 2))) < closest && foods[i] > 0) {
                    target = treegroup.GetChild(i);
                    closest = toFloat(Math.Sqrt(Math.Pow(currentDistance.x, 2) + Math.Pow(currentDistance.z, 2)));
                    place = i;
                } 
            } 
            initialSpeed = new Vector3((self.transform.position.x - target.position.x) / movementSpeed, 0, (self.transform.position.z - target.position.z) / movementSpeed);
        } else {
            if (getDistance(self.transform.position, target.transform.position) < 1 && basket < 10) {
                other.spawnBerry(this.transform);
                foods[place] -= 1;
                basket++;
            } else if (getDistance(self.transform.position, homePosition) < 1 && basket > 0) {
                other.deSpawnBerry(barrel.transform);
                basket -= 1;
                berries += 1;
            }else if (basket == 0) {
                target = null;
                StartCoroutine(Path());
            } else {
                bool greenLight = true;
                foreach (GameObject g in other.childrens) {
                    if (g.transform.position.y > bowl.transform.position.y) {
                        greenLight = false;
                    } else {
                        g.transform.parent = this.transform;
                    }
                }
                if (greenLight == true) {
                    initialSpeed = new Vector3((self.transform.position.x - homePosition.x) / movementSpeed, 0, (self.transform.position.z - homePosition.z) / movementSpeed);
                    StartCoroutine(Path());
                }
            }
        }
    }
    private float toFloat(double a) {
        return (float)a;
    }

    float getDistance (Vector3 a, Vector3 b) {
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
