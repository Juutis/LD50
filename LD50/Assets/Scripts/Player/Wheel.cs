using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public Transform visuals;
    WheelCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyVisuals();
    }

    void FixedUpdate() {
    }

    public void ApplyVisuals()
    {     
        Vector3 position;
        Quaternion rotation;
        coll.GetWorldPose(out position, out rotation);
     
        visuals.transform.position = position;
        visuals.transform.rotation = rotation;
    }
}
