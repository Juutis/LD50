using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NpcDriverSpawner : MonoBehaviour
{
    List<Waypoint> Waypoints;
    public List<Waypoint> IllegalWaypoints;

    public int Count;
    public NpcCar Prefab;
    // Start is called before the first frame update
    void Start()
    {
        Waypoints = GetComponentsInChildren<Waypoint>().ToList();
        foreach(var wp in IllegalWaypoints) {
            Waypoints.Remove(wp);
        }

        for (int i = 0; i < Count; i++) {
            NpcCar car = Instantiate(Prefab);
            var randomWp = Waypoints[Random.Range(0, Waypoints.Count)];
            Waypoints.Remove(randomWp);
            car.transform.position = randomWp.transform.position + Vector3.up;
            car.StartWayPoint = randomWp.Next();

            foreach(var wp in getCloseBy(randomWp)) {
                Waypoints.Remove(wp);
            }
            if (Waypoints.Count == 0) {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<Waypoint> getCloseBy(Waypoint wp) {
        List<Waypoint> result = new List<Waypoint>();
        foreach(var candidate in Waypoints) {
            if (Vector3.Distance(candidate.transform.position, wp.transform.position) < 50.0f) {
                result.Add(candidate);
            }
        }
        return result;
    }
}
