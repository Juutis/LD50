using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCar : MonoBehaviour
{
    Rigidbody rb;
    public Waypoint StartWayPoint;
    Waypoint current, next;

    float speed = 25.0f;
    float rotateSpeed = 0.25f;

    bool active = true;

    AudioSource engineSound;
    float maxPitch = 2.0f;
    float minPitch = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        current = StartWayPoint;
        next = current.Next();
        level = LayerMask.NameToLayer("Level");
        transform.forward = next.transform.position - transform.position;
        engineSound = GetComponent<AudioSource>();
        Invoke("SoundOn", 1.0f);
    }

    public void SoundOn() {
        engineSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        var targetDir = current.transform.position - transform.position;
        targetDir.y = 0.0f;

        var angleDiff = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        var sign = Mathf.Sign(angleDiff);
        var rotate = Mathf.Abs(angleDiff) * Time.deltaTime * rotateSpeed * rb.velocity.magnitude;
        rotate = Mathf.Min(Mathf.Abs(angleDiff), rotate) * sign;
        transform.Rotate(Vector3.up, rotate);
        //transform.forward = targetDir;

        var t = rb.velocity.magnitude / 100.0f;
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, t);
    }

    void FixedUpdate() {
        if (!active) return;

        if (current != null) {
            var dir = transform.forward;
            dir.y = 0;
            rb.velocity = dir.normalized * speed;
            //rb.AddForce(dir.normalized * speed, ForceMode.Acceleration);
        }
        if (Vector3.Distance(transform.position, current.transform.position) < 5.0f) {
            if (next != null) {
                var cur = next;
                next = next.Next();
                current = cur;
            }
        }
    }

    int level;

    void OnCollisionEnter(Collision collider) {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Driver") {
        //if (collider.gameObject.layer != level) {
            active = false;
        }
    }
}
