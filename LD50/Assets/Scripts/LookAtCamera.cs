using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        var cam = Camera.main;
        Vector3 v = cam.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt( cam.transform.position - v ); 
        transform.Rotate(0,180,0);
    }
}
