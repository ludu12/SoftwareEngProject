using UnityEngine;
using System.Collections;

public class SimpleCarMotorController : MonoBehaviour
{
    private IMovementController movementController;

    public void ControlCar()
    {
        movementController.Drive();
    }

    public void ControlWheelRotation()
    {
        movementController.RotateWheels();
    }
    // set movement controller
    public void SetMovementController(IMovementController movementController)
    {
        this.movementController = movementController;
    }
}
