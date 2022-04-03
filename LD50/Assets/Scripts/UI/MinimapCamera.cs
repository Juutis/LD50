using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    GameObject player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + offset;
    }
}
