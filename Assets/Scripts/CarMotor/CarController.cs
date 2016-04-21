using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour, ICarMovementController, ICarAudioController
{
    [SerializeField]
    private WheelCollider[] m_WheelColliders = new WheelCollider[4];
    [SerializeField]
    private GameObject[] m_WheelMeshes = new GameObject[4];
    [SerializeField]
    private UnityStandardAssets.Vehicles.Car.WheelEffects[] m_WheelEffects = new UnityStandardAssets.Vehicles.Car.WheelEffects[4];
    [SerializeField]
    private Vector3 m_CentreOfMassOffset;
    [SerializeField]
    private float m_MaximumSteerAngle;
    [Range(0, 1)]
    [SerializeField]
    private float m_SteerHelper; // 0 is raw physics , 1 the car will grip in the direction it is facing
    [Range(0, 1)]
    [SerializeField]
    private float m_TractionControl; // 0 is no traction control, 1 is full interference
    [SerializeField]
    private float m_FullTorqueOverAllWheels;
    [SerializeField]
    private float m_ReverseTorque;
    [SerializeField]
    private float m_MaxHandbrakeTorque;
    [SerializeField]
    private float m_Downforce = 100f;
    [SerializeField]
    private float m_Topspeed = 200;
    [SerializeField]
    private static int NoOfGears = 5;
    [SerializeField]
    private float m_RevRangeBoundary = 1f;
    [SerializeField]
    private float m_SlipLimit;
    [SerializeField]
    private float m_BrakeTorque;

    public bool canMove = false;

    public AudioClip lowAccelClip;                                              // Audio clip for low acceleration
    public AudioClip lowDecelClip;                                              // Audio clip for low deceleration
    public AudioClip highAccelClip;                                             // Audio clip for high acceleration
    public AudioClip highDecelClip;                                             // Audio clip for high deceleration
    public float pitchMultiplier = 0.8f;                                          // Used for altering the pitch of audio clips
    public float lowPitchMin = 1f;                                              // The lowest possible pitch for the low sounds
    public float lowPitchMax = 5f;                                              // The highest possible pitch for the low sounds
    public float highPitchMultiplier = 0.25f;                                   // Used for altering the pitch of high sounds

    private AudioSource m_LowAccel; // Source for the low acceleration sounds
    private AudioSource m_LowDecel; // Source for the low deceleration sounds
    private AudioSource m_HighAccel; // Source for the high acceleration sounds
    private AudioSource m_HighDecel; // Source for the high deceleration sounds

    private Rigidbody m_Rigidbody;

    private CarMotor m_Car; // the car motor we want to use

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Car = new CarMotor();
        m_Car.SetCarMovementInterface(this);
        m_Car.InitializeCarMotor(m_CentreOfMassOffset, m_MaximumSteerAngle, m_SteerHelper, m_TractionControl, m_FullTorqueOverAllWheels, m_ReverseTorque, m_Topspeed, m_RevRangeBoundary, m_SlipLimit, m_BrakeTorque);
        m_Car.SetCarAudioInterface(this);
        m_Car.InitializeCarAudio(lowPitchMin, lowPitchMax);
        m_Car.StartSound();
    }

    private void Update()
    {
        m_Car.CarAudio();
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        // pass the input to the car!
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float handbrake = Input.GetAxis("Jump");
        m_Car.Move(h, v, v, handbrake);
    }

    #region Car movemenet interface implementation

    public void SpinWheels()
    {
        //Move wheels 
        m_WheelMeshes[0].transform.Rotate(-m_WheelColliders[0].rpm / 60 * 360 * Time.deltaTime, 0, 0);
        m_WheelMeshes[1].transform.Rotate(m_WheelColliders[1].rpm / 60 * 360 * Time.deltaTime, 0, 0);
        m_WheelMeshes[2].transform.Rotate(-m_WheelColliders[2].rpm / 60 * 360 * Time.deltaTime, 0, 0);
        m_WheelMeshes[3].transform.Rotate(m_WheelColliders[3].rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }

    public void RotateFrontTires(float steerAngle)
    {
        m_WheelColliders[0].steerAngle = steerAngle;
        m_WheelColliders[1].steerAngle = steerAngle;
    }

    public void ApplyHandBrake(float handBrakeTorque)
    {
        //Assuming that wheels 3 and 4 are the rear wheels.
        m_WheelColliders[2].brakeTorque = handBrakeTorque;
        m_WheelColliders[3].brakeTorque = handBrakeTorque;
    }

    public void AddDownForce(float downForce)
    {
        m_WheelColliders[0].attachedRigidbody.AddForce(-transform.up * downForce * m_WheelColliders[0].attachedRigidbody.velocity.magnitude);
    }

    public float SteerHelper(float oldRotation, float steerHelper)
    {
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelhit;
            m_WheelColliders[i].GetGroundHit(out wheelhit);
            if (wheelhit.normal == Vector3.zero)
                return oldRotation; // wheels arent on the ground so dont realign the rigidbody velocity
        }

        // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
        if (Mathf.Abs(oldRotation - transform.eulerAngles.y) < 10f)
        {
            var turnadjust = (transform.eulerAngles.y - oldRotation) * m_SteerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
            m_Rigidbody.velocity = velRotation * m_Rigidbody.velocity;
        }

        return transform.eulerAngles.y;
    }

    public float GetRigidbodyVelocity()
    {
        return m_Rigidbody.velocity.magnitude;
    }

    public void SetRigidbodyVelocity(float velocity)
    {
        m_Rigidbody.velocity = velocity * m_Rigidbody.velocity.normalized;
    }

    public void ApplyMotorTorque(float torque)
    {
        // Iterate through all wheels and change torque
        for (int i = 0; i < 4; i++)
            m_WheelColliders[i].motorTorque = torque;
    }

    public void ApplyBrakeTorue(float torque)
    {
        for (int i = 0; i < 4; i++)
            m_WheelColliders[i].brakeTorque = torque;
    }

    public float GetAngleBetweenVelocityAndForward()
    {
        return Vector3.Angle(transform.forward, m_Rigidbody.velocity);
    }

    public void SetWheelColliderCenterOfMass(Vector3 centerOfMassOffset)
    {
        m_WheelColliders[0].attachedRigidbody.centerOfMass = centerOfMassOffset;
    }

    public float GetWheelColliderForwardSlip(int index)
    {
        WheelHit wheelHit;
        m_WheelColliders[index].GetGroundHit(out wheelHit);
        return wheelHit.forwardSlip;
    }

    public float GetWheelColliderSidewaysSlip(int index)
    {
        WheelHit wheelHit;
        m_WheelColliders[index].GetGroundHit(out wheelHit);
        return wheelHit.sidewaysSlip;
    }

    public void EmitTireSmoke(int index)
    {
        m_WheelEffects[index].EmitTyreSmoke();
    }

    public void EndSkidTrail(int index)
    {
        m_WheelEffects[index].EndSkidTrail();
    }

    #endregion

    #region Car Audio interface implementation

    public void StartSound()
    {
        // setup the simple audio source
        m_HighAccel = SetUpEngineAudioSource(highAccelClip);
        m_LowAccel = SetUpEngineAudioSource(lowAccelClip);
        m_LowDecel = SetUpEngineAudioSource(lowDecelClip);
        m_HighDecel = SetUpEngineAudioSource(highDecelClip);
    }
    // sets up and adds new audio source to the gane object
    private AudioSource SetUpEngineAudioSource(AudioClip clip)
    {
        // create the new audio source component on the game object and set up its properties
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.loop = true;

        // start the clip from a random point
        source.time = Random.Range(0f, clip.length);
        source.Play();
        source.minDistance = 5;
        return source;
    }

    public void StopSound()
    {
        //Destroy all audio sources on this object:
        foreach (var source in GetComponents<AudioSource>())
        {
            Destroy(source);
        }
    }

    public void AdjustPitch(float pitch)
    {
        m_LowAccel.pitch = pitch * pitchMultiplier;
        m_LowDecel.pitch = pitch * pitchMultiplier;
        m_HighAccel.pitch = pitch * highPitchMultiplier * pitchMultiplier;
        m_HighDecel.pitch = pitch * highPitchMultiplier * pitchMultiplier;
    }

    public void AdjustVolumes(float lowFade, float highFade, float decFade, float accFade)
    {
        m_LowAccel.volume = lowFade * accFade;
        m_LowDecel.volume = lowFade * decFade;
        m_HighAccel.volume = highFade * accFade;
        m_HighDecel.volume = highFade * decFade;
    }
    #endregion

    void OnDestory()
    {
        m_Car.StopSound();
    }
}
