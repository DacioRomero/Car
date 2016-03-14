using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody)), DisallowMultipleComponent]
public class CarController : MonoBehaviour
{
    public float motorTorque;

    [System.Serializable]
    public class Wheel
    {
        public string name;
        public WheelCollider wheelCollider;
        public Transform meshTransfrom;
        public ParticleSystem smokeParticles;
        public float steerAngle;
        public bool powered;
        public float brakeTorque;

        public Wheel()
        {
            name = "Wheel";
        }
    }

    public Wheel[] wheels;

    void Awake()
    {
        foreach (Wheel wheel in wheels)
        {
            wheel.wheelCollider.ConfigureVehicleSubsteps(8, 32, 64);
        }
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool braking = Input.GetButton("Jump");

        int poweredWheels = 0;
        foreach (Wheel wheel in wheels)
        {
            poweredWheels += wheel.powered ? 1 : 0;
        }

        float torqueRatio = 1f / poweredWheels;

        WheelHit wheelHit;
        Vector3 wheelPosition;
        Quaternion wheelRotation;

        foreach (Wheel wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = wheel.powered ? motorTorque * torqueRatio * input.y : 0;
            wheel.wheelCollider.brakeTorque = braking ? wheel.brakeTorque : 0;
            wheel.wheelCollider.steerAngle = wheel.steerAngle * input.x;

            wheel.wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);
            wheel.meshTransfrom.position = wheelPosition;
            wheel.meshTransfrom.rotation = wheelRotation;

            wheel.smokeParticles.startLifetime = wheel.wheelCollider.GetGroundHit(out wheelHit) ? Mathf.Abs(wheelHit.forwardSlip) + Mathf.Abs(wheelHit.sidewaysSlip) : 0;
        }
    }
}
