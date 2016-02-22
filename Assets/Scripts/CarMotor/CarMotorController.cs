using UnityEngine;
using System.Collections;

public class CarMotorController
{

    //Car speed
    public float speed = 0f;
    //Car limits
    public float maxSpeed = 15f;
    public float maxReverseSpeed = -10f;
    public float accel = 0.1f;
    public float decel = 0.15f;
    public float rotationSpeed = 45f;

    private IMovementController movementController;

    public void MoveForward()
    {
        // accel, check for max speed
        speed += accel;
        if (speed > maxSpeed)
            speed = maxSpeed;

        movementController.Translate(speed);
    }

    public void MoveBackward()
    {
        // decel
        speed -= decel;
        if (speed > 0)
            // translate car
            movementController.Translate(speed);
        else
        {
            // reverse
            if (speed < maxReverseSpeed)
                speed = maxReverseSpeed;
            movementController.Translate(speed);
        }

    }

    public void TurnRight()
    {
        if (speed != 0)
            movementController.Rotate(rotationSpeed);
    }

    public void TurnLeft()
    {
        if (speed != 0)
            // NOTE: since we are turning left, we are negating the rotation speed
            movementController.Rotate(-rotationSpeed);
    }

    public void SlowDown()
    {
        if (speed < 0)
        {
            // if we are going in reverse
            speed += decel;
            if (speed > 0)
                speed = 0;

            movementController.Translate(speed);
        }
        else
        {
            // if we are moving forward
            speed -= decel;
            if (speed < 0)
                speed = 0;

            movementController.Translate(speed);
        }
    }


    // set movement controller
    public void SetMovementController(IMovementController movementController)
    {
        this.movementController = movementController;
    }
}
