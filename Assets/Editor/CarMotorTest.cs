using System;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using System.Reflection;

[TestFixture]
[Category("Car")]
public class CarMotorTest
{
    [Test]
    public void InitializeCarMotor()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        car.SetCarMovementInterface(movement);
        Vector3 CoM = new Vector3(1, 1, 1);

        //Act
        car.InitializeCarMotor(CoM, 25f, 0.7f, 0.5f, 2500f, 500f, 100f, 1f, 0.3f, 20000f);

        //Assert
        movement.Received().SetWheelColliderCenterOfMass(CoM);
    }


    [Test]
    public void MoveClamping()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        car.SetCarMovementInterface(movement);
        car.InitializeCarMotor(new Vector3(0, 0, 0), 25f, 0.7f, 0.5f, 2500f, 500f, 100f, 1f, 0.3f, 20000f);

        //Act
        car.Move(0, 25, -25, 0);

        //Assert
        Assert.AreEqual(1f, car.AccelInput);
        Assert.AreEqual(-1f, -car.BrakeInput);
    }

    [Test]
    public void MoveRotateTires()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        car.SetCarMovementInterface(movement);
        car.InitializeCarMotor(new Vector3(0, 0, 0), 25f, 0.7f, 0.5f, 2500f, 500f, 100f, 1f, 0.3f, 20000f);
        float maxSteerAngle = (float)GetInstanceField(typeof(CarMotor), car, "m_MaximumSteerAngle");
        float steering = 0.5f;

        //Act
        car.Move(steering, 0, 0, 0);

        //Assert
        movement.Received().RotateFrontTires(steering * maxSteerAngle);
    }

    [Test]
    public void MoveApplyDrive()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        car.SetCarMovementInterface(movement);
        car.InitializeCarMotor(new Vector3(0, 0, 0), 25f, 0.7f, 0.5f, 2500f, 500f, 100f, 1f, 0.3f, 20000f);
        float t = (float)GetInstanceField(typeof(CarMotor), car, "m_CurrentTorque");
        float accel = 0.5f;

        //Act
        car.Move(0, accel, 0, 0);

        //Assert
        movement.Received().ApplyMotorTorque(accel * t / 4); // t in this case is the total torque over all 4 wheels so divide by 4
    }

    [Test]
    public void MoveCapSpeed()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        movement.GetRigidbodyVelocity().Returns(100f); // this will return a velocity magnitude equal to our top speed variable
        car.SetCarMovementInterface(movement);
        float topSpeed = 100f;
        car.InitializeCarMotor(new Vector3(0, 0, 0), 25f, 0.7f, 0.5f, 2500f, 500f, topSpeed, 1f, 0.3f, 20000f);
        float mphConst = (float)GetInstanceField(typeof(CarMotor), car, "k_MphConst");

        //Act
        car.Move(0, 1, 0, 0);

        //Assert
        movement.Received().SetRigidbodyVelocity(topSpeed/mphConst); // we should see the velocity being reset
    }

    [Test]
    public void MoveDidNotApplyHandbrake()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        car.SetCarMovementInterface(movement);

        //Act
        car.Move(0, 0, 0, 0);

        //Assert
        movement.DidNotReceive().ApplyHandBrake(Arg.Any<float>()); // we should see the velocity being reset
    }

    [Test]
    public void MoveDidApplyHandbrake()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        car.SetCarMovementInterface(movement);

        //Act
        car.Move(0, 0, 0, 1);

        //Assert
        movement.Received().ApplyHandBrake(Arg.Any<float>()); // we should see the velocity being reset
    }

    [Test]
    public void MoveAddDownforce()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        car.SetCarMovementInterface(movement);
        float dF = (float)GetInstanceField(typeof(CarMotor), car, "m_Downforce");

        //Act
        car.Move(0, 0, 0, 1);

        //Assert
        movement.Received().AddDownForce(dF); // we should see the velocity being reset
    }

    [Test]
    public void MoveCheckForWheelSpinTireSmoke()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        movement.GetWheelColliderForwardSlip(Arg.Any<int>()).Returns(1); // more then slip limit
        movement.GetWheelColliderSidewaysSlip(Arg.Any<int>()).Returns(1); // more then slip limit
        car.SetCarMovementInterface(movement);
        float slipLimit = 0.3f;
        car.InitializeCarMotor(new Vector3(0, 0, 0), 25f, 0.7f, 0.5f, 2500f, 500f, 100f, 1f, slipLimit, 20000f);

        //Act
        car.Move(0, 0, 0, 1);

        //Assert
        movement.Received(4).EmitTireSmoke(Arg.Any<int>()); // we should the tire smoke emitted 4 times
    }

    [Test]
    public void MoveCheckForWheelSpinEndTrail()
    {
        //Arrange
        CarMotor car = new CarMotor();
        var movement = GetMovementMock();
        movement.GetWheelColliderForwardSlip(Arg.Any<int>()).Returns(0); // less then slip limit
        movement.GetWheelColliderSidewaysSlip(Arg.Any<int>()).Returns(0); // less then slip limit
        car.SetCarMovementInterface(movement);
        float slipLimit = 0.3f;
        car.InitializeCarMotor(new Vector3(0, 0, 0), 25f, 0.7f, 0.5f, 2500f, 500f, 100f, 1f, slipLimit, 20000f);

        //Act
        car.Move(0, 0, 0, 1);

        //Assert
        movement.Received(4).EndSkidTrail(Arg.Any<int>()); // we should the skid trail ending 4 times
    }

    [Test]
    public void CarAudio()
    {
        // Arrange
        CarMotor car = new CarMotor();
        var audio = GetAudioMock();
        car.SetCarAudioInterface(audio);
        car.InitializeCarAudio(0.5f, 0.5f);

        //Act
        car.CarAudio();

        //Assert
        audio.Received().AdjustPitch(Arg.Any<float>());
        audio.Received().AdjustVolumes(Arg.Any<float>(), Arg.Any<float>(), Arg.Any<float>(), Arg.Any<float>());
    }

    [Test]
    public void StartSound()
    {
        // Arrange
        CarMotor car = new CarMotor();
        var audio = GetAudioMock();
        car.SetCarAudioInterface(audio);

        //Act
        car.StartSound();

        //Assert
        audio.Received().StartSound();
    }

    [Test]
    public void StopSound()
    {
        // Arrange
        CarMotor car = new CarMotor();
        var audio = GetAudioMock();
        car.SetCarAudioInterface(audio);

        //Act
        car.StopSound();

        //Assert
        audio.Received().StopSound();
    }

    private ICarMovementController GetMovementMock ()
    {
        return Substitute.For<ICarMovementController>();
    }

    private ICarAudioController GetAudioMock()
    {
        return Substitute.For<ICarAudioController>();
    }

    /// <summary>
    /// Uses reflection to get the field value from an object.
    /// </summary>
    ///
    /// <param name="type">The instance type.</param>
    /// <param name="instance">The instance object.</param>
    /// <param name="fieldName">The field's name which is to be fetched.</param>
    ///
    /// <returns>The field value from the object.</returns>
    internal static object GetInstanceField(Type type, object instance, string fieldName)
    {
        BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            | BindingFlags.Static;
        FieldInfo field = type.GetField(fieldName, bindFlags);
        return field.GetValue(instance);
    }
}