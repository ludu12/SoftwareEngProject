using UnityEngine;
using System.Collections;

public interface ICarMovementController
{
    void SpinWheels();
    void RotateFrontTires(float steerAngle);
    void SetWheelColliderCenterOfMass(Vector3 centerOfMassOffset);
    float GetWheelColliderForwardSlip(int index);
    float GetWheelColliderSidewaysSlip(int index);
    void EmitTireSmoke(int index);
    void EndSkidTrail(int index);

    void ApplyHandBrake(float handBrakeTorque);
    void AddDownForce(float downForce);
    float SteerHelper(float oldRotation, float steerHelper);

    void ApplyMotorTorque(float torque);
    void ApplyBrakeTorue(float torque);

    float GetRigidbodyVelocity();
    void SetRigidbodyVelocity(float velocity);
    float GetAngleBetweenVelocityAndForward();
}
