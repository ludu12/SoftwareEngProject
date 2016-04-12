using UnityEngine;
using System.Collections;
using System;

public class CarMotorOld : MonoBehaviour, IMovementController
{
    private SimpleCarMotorController carController;

    //Wheel Colliders
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider backLeftCollider;
    public WheelCollider backRightCollider;

    //Wheel gameobjects
    public GameObject frontLeftWheel;
    public GameObject frontRightWheel;
    public GameObject backLeftWheel;
    public GameObject backRightWheel;

    //Speed,braking,turning
    public float speed = 100f;
    public float braking = 100f;
    public float turning = 30f;

    private void OnEnable()
    {
        // Set the Movement interface for the car controller to be this
        carController = new SimpleCarMotorController();
        carController.SetMovementController(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCar();
        MoveWheels();
    }

    public void MoveCar()
    {
        carController.ControlCar();
    }

    public void MoveWheels()
    {
        carController.ControlWheelRotation();
    }

    public void Drive()
    {
        //Moving the car
        backLeftCollider.motorTorque = Input.GetAxis("Vertical") * speed;
        backRightCollider.motorTorque = Input.GetAxis("Vertical") * speed;

        //Set brakeTorque to zero to ensure not braking anymore
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
    }

    public void RotateWheels()
    {
        //Turn wheels
        frontLeftWheel.transform.localEulerAngles = new Vector3(frontLeftWheel.transform.localEulerAngles.x, -(180 - (frontLeftCollider.steerAngle - frontLeftWheel.transform.localEulerAngles.z)), frontLeftWheel.transform.localEulerAngles.z);
        frontRightWheel.transform.localEulerAngles = new Vector3(frontRightWheel.transform.localEulerAngles.x, frontRightCollider.steerAngle - frontRightWheel.transform.localEulerAngles.z, frontRightWheel.transform.localEulerAngles.z);

        //Move wheels 
        frontLeftWheel.transform.Rotate(frontLeftCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        frontRightWheel.transform.Rotate(frontRightCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        backLeftWheel.transform.Rotate(backLeftCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        backRightWheel.transform.Rotate(backRightCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
}
