using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapArrow : MonoBehaviour
{
    Transform target;
    Transform player;
    float minDistance = 220.0f;

    Renderer rend;
    public Transform arrow;

    public bool Show;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rend = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetDir = target.position - player.position;
        targetDir.y = 0;
        if (!Show || targetDir.magnitude < minDistance) {
            rend.enabled = false;
        } else {
            rend.enabled = true;
            transform.position = player.position + targetDir.normalized * minDistance + Vector3.up * 50.0f;
            arrow.right = targetDir;
        }
    }
}
