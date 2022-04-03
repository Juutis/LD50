using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> Waypoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Waypoint Next() {
        if (Waypoints.Count == 0) {
            Debug.LogError("NULL WAYPOINT", this);
            return null;
        } 
        return Waypoints[Random.Range(0, Waypoints.Count)];
    }

    void OnDrawGizmos() {
        foreach(var i in Waypoints) {
            Debug.DrawLine(transform.position, i.transform.position, Color.red);
        }
    }
}
