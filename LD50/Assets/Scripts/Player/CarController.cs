using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxMotorTorquePerLevel;
    public float maxSteeringAngle;
    public float brakeTorque;
    public float lockBrakeTorque;

    AudioSource engineSound;
    Rigidbody rb;

    public float maxPitch = 2.0f;
    public float minPitch = 0.25f;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        engineSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var t = getMaxRpm() / 10000.0f;
        var t = rb.velocity.magnitude / 100.0f;
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, t);

        if (transform.position.y < -15.0f) {
            GameManager.main.GameOver();
            dead = true;
        }
    }

    public void FixedUpdate()
    {
        float baseMotor = (maxMotorTorque + maxMotorTorquePerLevel * GameManager.main.State.EngineLevel);
        var boost = (25 - rb.velocity.magnitude) / 25.0f;
        //float motor = Mathf.Lerp(baseMotor, maxMotorTorque + maxMotorTorquePerLevel * 3, Mathf.Clamp(boost, 0, 1)) * Input.GetAxis("Vertical");
        float motor = Mathf.Lerp(baseMotor, baseMotor * 5, Mathf.Clamp(boost, 0, 1)) * Input.GetAxis("Vertical");


        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        bool lockBrake = Input.GetKey(KeyCode.Space);

        if (dead) {
            motor = 0;
            steering = 0;
        }
            
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                if (Mathf.Sign(axleInfo.leftWheel.motorTorque) != Mathf.Sign(motor)) {
                    axleInfo.leftWheel.brakeTorque = brakeTorque;
                    axleInfo.leftWheel.motorTorque = motor;
                } else {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.leftWheel.motorTorque = motor;
                }
                if (Mathf.Sign(axleInfo.rightWheel.motorTorque) != Mathf.Sign(motor)) {
                    axleInfo.rightWheel.brakeTorque = brakeTorque;
                    axleInfo.rightWheel.motorTorque = motor;
                } else {
                    axleInfo.rightWheel.brakeTorque = 0;
                    axleInfo.rightWheel.motorTorque = motor;
                }
            }
            if (axleInfo.lockBrake) {
                if (lockBrake) {
                    axleInfo.leftWheel.brakeTorque = lockBrakeTorque;
                    axleInfo.rightWheel.brakeTorque = lockBrakeTorque;
                    setStiffness(axleInfo.leftWheel, 0.1f);
                    setStiffness(axleInfo.rightWheel, 0.1f);
                } else {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.rightWheel.brakeTorque = 0;

                    setStiffness(axleInfo.leftWheel, 1.0f);
                    setStiffness(axleInfo.rightWheel, 1.0f);
                }
            }
        }
    }

    float getMaxRpm() {
        var result = 0.0f;
        foreach (AxleInfo axleInfo in axleInfos) {
            var maxRpm = Mathf.Max(axleInfo.leftWheel.rpm, axleInfo.rightWheel.rpm);
            result = Mathf.Max(result, maxRpm);
        }
        return result;
    }

    private void setStiffness(WheelCollider wheel, float stiffness) {
                            
        var sideFriction = wheel.sidewaysFriction;
        sideFriction.stiffness = stiffness;
        wheel.sidewaysFriction = sideFriction;

        var forwardFriction = wheel.forwardFriction;
        forwardFriction.stiffness = stiffness;
        wheel.forwardFriction = forwardFriction;
    }
}

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
    public bool lockBrake;
}