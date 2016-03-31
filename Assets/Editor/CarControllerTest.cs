using System;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class CarControllerTest
{
    [Test]
    public void Drive()
    {
        //Arrange
        SimpleCarMotorController car = new SimpleCarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);

        //Act
        car.ControlCar();
        
        //Assert
        movement.Received().Drive();
    }

    [Test]
    public void ControlWheelRotation()
    {
        //Arrange
        SimpleCarMotorController car = new SimpleCarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        
        //Act
        car.ControlWheelRotation();

        //Assert
        movement.Received().RotateWheels();
    }

    private IMovementController GetMovementMock ()
    {
        return Substitute.For<IMovementController>();
    }
}