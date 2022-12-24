using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float sidetoside = Input.GetAxisRaw("Horizontal");
        transform.Translate(0, 0, forward / 90);
        transform.Rotate(0, sidetoside / 8, 0);
        if (Input.GetKeyDown("space")) {
            print("space");
            transform.Translate(0, 2, 0);
        }
    }
}
