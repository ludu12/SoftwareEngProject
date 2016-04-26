using System;
using UnityEngine;

public class CarMotor
{
    // CAR STUFF SET BY CONTROLLER
    private Vector3 m_CentreOfMassOffset;
    private float m_MaximumSteerAngle;
    private float m_SteerHelper; // 0 is raw physics , 1 the car will grip in the direction it is facing
    private float m_TractionControl; // 0 is no traction control, 1 is full interference
    private float m_FullTorqueOverAllWheels;
    private float m_ReverseTorque;
    private float m_MaxHandbrakeTorque;
    private float m_Downforce = 100f;
    private float m_Topspeed = 200;
    private static int NoOfGears = 5;
    private float m_RevRangeBoundary = 1f;
    private float m_SlipLimit;
    private float m_BrakeTorque;
    // AUDIO STUFF SET BY CONTROLLER
    private float lowPitchMin;
    private float lowPitchMax;

    // INTERNAL PRIVATE
    private float m_SteerAngle;
    private int m_GearNum;
    private float m_GearFactor;
    private float m_OldRotation;
    private float m_CurrentTorque;
    private const float k_ReversingThreshold = 0.01f;
    private const float k_MphConst = 2.23693629f;

    // PROPERTIES
    public bool Skidding { get; private set; }
    public float BrakeInput { get; private set; }
    public float CurrentSteerAngle{ get { return m_SteerAngle; }}
    public float MaxSpeed{get { return m_Topspeed; }}
    public float Revs { get; private set; }
    public float AccelInput { get; private set; }

    ICarMovementController carMovementController;
    ICarAudioController carAudioController;

    // Use this for initialization
    public void InitializeCarMotor(Vector3 centerOfMass, float maxSteerAngle, float steerHelper, float tractionControl, float fullTorque, float reverseTorque, float topSpeed, float revRange, float slipLimit, float brakeTorque)
    {
        m_CentreOfMassOffset = centerOfMass;
        m_MaximumSteerAngle = maxSteerAngle;
        m_SteerHelper = steerHelper;
        m_TractionControl = tractionControl; 
        m_FullTorqueOverAllWheels = fullTorque;
        m_ReverseTorque = reverseTorque;
        m_Topspeed = topSpeed;
        m_RevRangeBoundary = revRange;
        m_SlipLimit = slipLimit;
        m_BrakeTorque = brakeTorque;

        carMovementController.SetWheelColliderCenterOfMass(m_CentreOfMassOffset);
        m_MaxHandbrakeTorque = float.MaxValue;
        m_CurrentTorque = m_FullTorqueOverAllWheels - (m_TractionControl * m_FullTorqueOverAllWheels);
    }

    public void InitializeCarAudio(float lowPitchMin, float lowPitchMax)
    {
        this.lowPitchMin = lowPitchMin;
        this.lowPitchMax = lowPitchMax;
    }

    /// <summary>
    /// This is the main function, it calls all other functions to effectively move the car
    /// </summary>
    /// <param name="steering"></param>
    /// <param name="accel"></param>
    /// <param name="footbrake"></param>
    /// <param name="handbrake"></param>
    public void Move(float steering, float accel, float footbrake, float handbrake)
    {
        carMovementController.SpinWheels();

        //clamp input values
        steering = Mathf.Clamp(steering, -1, 1);
        AccelInput = accel = Mathf.Clamp(accel, 0, 1);
        BrakeInput = footbrake = -1*Mathf.Clamp(footbrake, -1, 0);
        handbrake = Mathf.Clamp(handbrake, 0, 1);

        //Set the steer on the front wheels.
        m_SteerAngle = steering*m_MaximumSteerAngle;
        carMovementController.RotateFrontTires(m_SteerAngle);

        m_OldRotation = carMovementController.SteerHelper(m_OldRotation, m_SteerHelper);
        ApplyDrive(accel, footbrake);
        CapSpeed();

        //Set the handbrake.
        //Assuming that wheels 2 and 3 are the rear wheels.
        if (handbrake > 0f)
        {
            float hbTorque = handbrake*m_MaxHandbrakeTorque;
            carMovementController.ApplyHandBrake(hbTorque);
        }


        CalculateRevs();
        GearChanging();

        carMovementController.AddDownForce(m_Downforce);
        CheckForWheelSpin();
        TractionControl();
    }

    /// <summary>
    /// Calculates the brake and motor torques from the user input
    /// </summary>
    /// <param name="accel"></param>
    /// <param name="footbrake"></param>
    private void ApplyDrive(float accel, float footbrake)
    {
        // Calculate the how much we should accel by
        float thrustTorque;
        thrustTorque = accel * (m_CurrentTorque / 4f);
        carMovementController.ApplyMotorTorque(thrustTorque);

        if (carMovementController.GetRigidbodyVelocity() * k_MphConst > 5 && carMovementController.GetAngleBetweenVelocityAndForward() < 50f)
        {
            carMovementController.ApplyBrakeTorue(m_BrakeTorque * footbrake);
        }
        else if (footbrake > 0) // If we are braking apply a motor torque in opposite direction
        {
            carMovementController.ApplyBrakeTorue(0f);
            carMovementController.ApplyMotorTorque(-m_ReverseTorque * footbrake);
        }
    }

    private void CapSpeed()
    {
        float speed = carMovementController.GetRigidbodyVelocity();
        speed *= k_MphConst;
        if (speed > m_Topspeed)
            carMovementController.SetRigidbodyVelocity(m_Topspeed / k_MphConst);
    }

    private void CalculateRevs()
    {
        // calculate engine revs (for display / sound)
        // (this is done in retrospect - revs are not used in force/power calculations)
        CalculateGearFactor();
        var gearNumFactor = m_GearNum / (float)NoOfGears;
        var revsRangeMin = ULerp(0f, m_RevRangeBoundary, CurveFactor(gearNumFactor));
        var revsRangeMax = ULerp(m_RevRangeBoundary, 1f, gearNumFactor);
        Revs = ULerp(revsRangeMin, revsRangeMax, m_GearFactor);
    }

    // unclamped version of Lerp, to allow value to exceed the from-to range
    private static float ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }

    // simple function to add a curved bias towards 1 for a value in the 0-1 range
    private static float CurveFactor(float factor)
    {
        return 1 - (1 - factor) * (1 - factor);
    }

    private void CalculateGearFactor()
    {
        float f = (1 / (float)NoOfGears);
        // gear factor is a normalised representation of the current speed within the current gear's range of speeds.
        // We smooth towards the 'target' gear factor, so that revs don't instantly snap up or down when changing gear.
        var targetGearFactor = Mathf.InverseLerp(f * m_GearNum, f * (m_GearNum + 1), Mathf.Abs((carMovementController.GetRigidbodyVelocity() * k_MphConst) / MaxSpeed));
        m_GearFactor = Mathf.Lerp(m_GearFactor, targetGearFactor, Time.deltaTime * 5f);
    }

    private void GearChanging()
    {
        float f = Mathf.Abs(carMovementController.GetRigidbodyVelocity() * k_MphConst / MaxSpeed);
        float upgearlimit = (1 / (float)NoOfGears) * (m_GearNum + 1);
        float downgearlimit = (1 / (float)NoOfGears) * m_GearNum;

        if (m_GearNum > 0 && f < downgearlimit)
        {
            m_GearNum--;
        }

        if (f > upgearlimit && (m_GearNum < (NoOfGears - 1)))
        {
            m_GearNum++;
        }
    }

    // checks if the wheels are spinning and is so does three things
    // 1) emits particles
    // 2) leaves skidmarks on the ground
    // these effects are controlled through the WheelEffects class
    private void CheckForWheelSpin()
    {
        // loop through all wheels
        for (int i = 0; i < 4; i++)
        {
            // is the tire slipping above the given threshhold
            if (Mathf.Abs(carMovementController.GetWheelColliderForwardSlip(i)) >= m_SlipLimit || Mathf.Abs(carMovementController.GetWheelColliderSidewaysSlip(i)) >= m_SlipLimit)
            {
                carMovementController.EmitTireSmoke(i);
                continue;
            }
            // end the trail generation
            carMovementController.EndSkidTrail(i);
        }
    }

    // crude traction control that reduces the power to wheel if the car is wheel spinning too much
    private void TractionControl()
    {
        // loop through all wheels
        for (int i = 0; i < 4; i++)
        {
            AdjustTorque(carMovementController.GetWheelColliderForwardSlip(i));
        }
    }

    private void AdjustTorque(float forwardSlip)
    {
        if (forwardSlip >= m_SlipLimit && m_CurrentTorque >= 0)
        {
            m_CurrentTorque -= 10 * m_TractionControl;
        }
        else
        {
            m_CurrentTorque += 10 * m_TractionControl;
            if (m_CurrentTorque > m_FullTorqueOverAllWheels)
            {
                m_CurrentTorque = m_FullTorqueOverAllWheels;
            }
        }
    }

    public void CarAudio()
    {
        // The pitch is interpolated between the min and max values, according to the car's revs.
        float pitch = ULerp(lowPitchMin, lowPitchMax, Revs);

        // clamp to minimum pitch (note, not clamped to max for high revs while burning out)
        pitch = Mathf.Min(lowPitchMax, pitch);

        // adjust the pitches based on the multipliers
        carAudioController.AdjustPitch(pitch);

        // get values for fading the sounds based on the acceleration
        float accFade = Mathf.Abs(AccelInput);
        float decFade = 1 - accFade;

        // get the high fade value based on the cars revs
        float highFade = Mathf.InverseLerp(0.2f, 0.8f, Revs);
        float lowFade = 1 - highFade;

        // adjust the values to be more realistic
        highFade = 1 - ((1 - highFade) * (1 - highFade));
        lowFade = 1 - ((1 - lowFade) * (1 - lowFade));
        accFade = 1 - ((1 - accFade) * (1 - accFade));
        decFade = 1 - ((1 - decFade) * (1 - decFade));

        // adjust the source volumes based on the fade values
        carAudioController.AdjustVolumes(lowFade, highFade, decFade, accFade);
    }

    public void StartSound()
    {
        carAudioController.StartSound();
    }

    public void StopSound()
    {
        carAudioController.StopSound();
    }

    /// <summary>
    /// Sets the interface so this script can be linked to the game
    /// </summary>
    /// <param name="carMovementController"></param>
    public void SetCarMovementInterface(ICarMovementController carMovementController)
    {
        this.carMovementController = carMovementController;
    }
    /// <summary>
    /// Sets the interface so this script can be linked to the game
    /// </summary>
    /// <param name="carMovementController"></param>
    public void SetCarAudioInterface(ICarAudioController carAudioController)
    {
        this.carAudioController = carAudioController;
    }
}
