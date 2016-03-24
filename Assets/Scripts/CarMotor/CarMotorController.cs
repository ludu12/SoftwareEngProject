using UnityEngine;
using System.Collections;

public class CarMotorController
{
    //Car limits
    public float rotationSpeed = 100f;

    private IMovementController movementController;

    public void MoveForward()
    {
        movementController.Translate(1);
    }

    public void MoveBackward()
    {
        movementController.Translate(-1);
    }

    public void TurnRight()
    {
        movementController.Rotate(rotationSpeed);
    }

    public void TurnLeft()
    {
        // NOTE: since we are turning left, we are negating the rotation speed
        movementController.Rotate(-rotationSpeed);
    }

    // set movement controller
    public void SetMovementController(IMovementController movementController)
    {
        this.movementController = movementController;
    }
}
