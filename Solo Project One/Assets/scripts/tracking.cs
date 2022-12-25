using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracking : MonoBehaviour
{
    public Transform treegroup;
    Vector3 currentDistance;
    currentDistance.x = treegroup.GetChild(0).transform.position.x;
    currentDistance.z = treegroup.GetChild(0).transform.position.z;
    int closest = Math.Sqrt(Math.Pow(currentDistance.x, 2) + Math.Pow(currentDistance.z, 2)) < closest;
    public gameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i< treegroup.childCount; i++) {
            currentDistance.x = treegroup.GetChild(i).transform.position.x;
            currentDistance.z = treegroup.GetChild(i).transform.position.z;
            if (Math.Sqrt(Math.Pow(currentDistance.x, 2) + Math.Pow(currentDistance.z, 2)) < closest) {
                target = treegroup.GetChild(i);
                closest = Math.Sqrt(Math.Pow(currentDistance.x, 2) + Math.Pow(currentDistance.z, 2));
            } 
        } 

    }
}
