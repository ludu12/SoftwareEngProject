using System;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;

[TestFixture] 
public class CarControllerTest
{

    /*[Test]
    public void SpeedUp()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);

        //Act
        car.MoveForward();

        //Assert
        
        Assert.Greater((decimal)car.speed, 0);
        movement.Received().Translate(0.1f);
    }

    [Test]
    public void ReachMaxSpeed()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = car.maxSpeed;

        //Act
        car.MoveForward();

        //Assert
        Assert.AreEqual((decimal)car.speed, (decimal)car.maxSpeed);
        movement.Received().Translate(car.maxSpeed);
    }

    [Test]
    public void SlowDown()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = 10f;

        //Act
        car.SlowDown();

        //Assert
        Assert.Less((decimal)car.speed, 10f);
        movement.Received().Translate(9.85f);
    }

    [Test]
    public void DoesNotMoveWhenStopped()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = 0f;

        //Act
        car.SlowDown();

        //Assert
        Assert.AreEqual((decimal)car.speed, 0);
        movement.Received().Translate(0f);
    }

    [Test]
    public void Reverses()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = -1f;

        //Act
        car.MoveBackward();

        //Assert
        Assert.Less((decimal)car.speed, -1);
        movement.Received().Translate(-1.15f);
    }

    [Test]
    public void ReachedMaxReverseSpeed()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = car.maxReverseSpeed;

        //Act
        car.MoveBackward();

        //Assert
        Assert.AreEqual((decimal)car.speed, (decimal)car.maxReverseSpeed);
        movement.Received().Translate(car.maxReverseSpeed);
    }

    [Test]
    public void TurnRightWhileMoving()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = 10f;

        //Act
        car.TurnRight();

        //Assert
        movement.Received().Rotate(45f);
    }

    [Test]
    public void TurnRightWhenNotMoving()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = 0f;

        //Act
        car.TurnRight();

        //Assert
        movement.DidNotReceive().Rotate(Arg.Any<float>());
    }

    [Test]
    public void TurnLeftWhileMoving()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = 10f;

        //Act
        car.TurnLeft();

        //Assert
        movement.Received().Rotate(-45f);
    }

    [Test]
    public void TurnLeftWhenNotMoving()
    {
        // Arrange
        CarMotorController car = new CarMotorController();
        var movement = GetMovementMock();
        car.SetMovementController(movement);
        car.speed = 0f;

        //Act
        car.TurnLeft();

        //Assert
        movement.DidNotReceive().Rotate(Arg.Any<float>());
    }


    private IMovementController GetMovementMock ()
    {
        return Substitute.For<IMovementController>();
<<<<<<< HEAD
    }*/

=======
    }
    */
>>>>>>> abdebb7defdc45a709371c176bc2b0a11aa34100
}
