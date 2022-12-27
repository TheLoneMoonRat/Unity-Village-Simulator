using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class becomeChild : MonoBehaviour
{
    public Transform tr;
    public GameObject me;
    // Start is called before the first frame update
    void Start()
    {
        me.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < tr.transform.position.x + 10 && this.transform.position.x > tr.transform.position.x - 10) {
            this.transform.parent = tr.transform;
        }
    }

    
}
