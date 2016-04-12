using System;
using UnityEngine;
public class CarController : MonoBehaviour
{
    private CarMotor m_Car; // the car controller we want to use

    private void Awake()
    {
        m_Car = new CarMotor();
    }

    private void FixedUpdate()
    {
        // pass the input to the car!
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float handbrake = Input.GetAxis("Jump");
        m_Car.Move(h, v, v, handbrake);
    }
}
