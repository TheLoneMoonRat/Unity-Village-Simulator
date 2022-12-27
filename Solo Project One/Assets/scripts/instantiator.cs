using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiator : MonoBehaviour
{
    public Transform tr;
    public Transform bowl;
    public GameObject me;
    public List<GameObject> burnList = new List <GameObject>();
    public List<GameObject> childrens = new List <GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        me.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void spawnBerry (Transform parenta) {
        Vector3 locationer = new Vector3(parenta.transform.position.x, parenta.transform.position.y + 1.2f, parenta.transform.position.z);
        childrens.Add(Instantiate(me, locationer, Quaternion.identity));
        foreach (GameObject go in childrens) {
            go.SetActive(true);
        }
    }
    public void deSpawnBerry (Transform parenta) {
        Vector3 locationer = new Vector3(parenta.transform.position.x, parenta.transform.position.y + 2, parenta.transform.position.z);
        foreach (GameObject clone in childrens) {
            tr.transform.DetachChildren();
            bowl.transform.parent = tr.transform;
            clone.transform.position = locationer;
        }
        burnList = childrens;
        childrens.Clear();
    }
}
