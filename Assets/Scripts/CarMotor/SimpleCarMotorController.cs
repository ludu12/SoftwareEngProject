using UnityEngine;
using System.Collections;

public class SimpleCarMotorController : MonoBehaviour
{
    //Wheel Colliders
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider backLeftCollider;
    public WheelCollider backRightCollider;

    public GameObject frontLeftWheel;
    public GameObject frontRightWheel;
    public GameObject backLeftWheel;
    public GameObject backRightWheel;

    //Speed,braking,turning
    public float speed = 1000f;
    public float braking = 5000f;
    public float turning = 45f;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Moving the car
        backLeftCollider.motorTorque = Input.GetAxis("Vertical") * speed;
        backRightCollider.motorTorque = Input.GetAxis("Vertical") * speed;

        backLeftCollider.brakeTorque = 0;
        backRightCollider.brakeTorque = 0;

        //Turning the car
        frontRightCollider.steerAngle = Input.GetAxis("Horizontal") * turning;
        frontLeftCollider.steerAngle = Input.GetAxis("Horizontal") * turning;

        //Braking
        if (Input.GetKey(KeyCode.Space))
        {
            backLeftCollider.brakeTorque = braking;
            backRightCollider.brakeTorque = braking;
        }

        frontLeftWheel.transform.localEulerAngles = new Vector3(frontLeftWheel.transform.localEulerAngles.x, -(180-(frontLeftCollider.steerAngle - frontLeftWheel.transform.localEulerAngles.z)), frontLeftWheel.transform.localEulerAngles.z);
        frontRightWheel.transform.localEulerAngles = new Vector3(frontRightWheel.transform.localEulerAngles.x, frontRightCollider.steerAngle - frontRightWheel.transform.localEulerAngles.z, frontRightWheel.transform.localEulerAngles.z);

        frontLeftWheel.transform.Rotate(frontLeftCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        frontRightWheel.transform.Rotate(frontRightCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        backLeftWheel.transform.Rotate(backLeftCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        backRightWheel.transform.Rotate(backRightCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
}
