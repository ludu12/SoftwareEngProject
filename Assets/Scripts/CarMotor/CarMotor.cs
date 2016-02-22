using UnityEngine;
using System.Collections;
using System;

public class CarMotor : MonoBehaviour, IMovementController {

    bool isDriving = false;
    public CarMotorController carController;

    private void OnEnable()
    {
        // Set the Movement interface for the car controller to be this
        carController = new CarMotorController();
        carController.SetMovementController(this);
    }


    void FixedUpdate()
    {
        //Keyboard inputs for controlling car
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            StartCoroutine(OnForward());
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            StartCoroutine(OnDown());
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            StartCoroutine(OnRight());
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            StartCoroutine(OnLeft());
        if (!isDriving)
            StartCoroutine(OnSlowDown());

        isDriving = false;
    }

    #region Enumerators

    // These enumerators call functions on the CarMotorController class and all logic is handled there

    public IEnumerator OnForward()
    {
        isDriving = true;
        carController.MoveForward();
        yield return null;
    }

    public IEnumerator OnRight()
    {
        carController.TurnRight();
        yield return null;
    }
    public IEnumerator OnLeft()
    {
        carController.TurnLeft();
        yield return null;
    }
    public IEnumerator OnDown()
    {
        isDriving = true;
        carController.MoveBackward();
        yield return null;
    }

    public IEnumerator OnSlowDown()
    {
        carController.SlowDown();
        yield return null;
    }

    #endregion

    #region Movement Implementation

    // This is where the car is actually moved or rotated

    public void Translate(float value)
    {
        Vector3 v = Vector3.forward * value * Time.deltaTime;
        transform.Translate(v);
    }

    public void Rotate(float value)
    {
        // Rotate around y axis
        Vector3 v = Vector3.up * value * Time.deltaTime;
        transform.Rotate(v);
    }
    #endregion
}
