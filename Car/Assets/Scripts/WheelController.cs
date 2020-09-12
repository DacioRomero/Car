using UnityEngine;

[DisallowMultipleComponent]
public class WheelController : MonoBehaviour
{
  public Transform meshTransform;
  public float maxTurn = 45;
  WheelCollider wheelCollider;
  public Quaternion rotationOffset;

  void Awake()
  {
    wheelCollider = GetComponent<WheelCollider>();
    rotationOffset = meshTransform.localRotation;
  }

  void FixedUpdate()
  {
    Vector3 wheelPosition;
    Quaternion wheelRotation;
    wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);
    meshTransform.position = wheelPosition;
    meshTransform.rotation = wheelRotation;
    meshTransform.localRotation *= rotationOffset;
  }

  public void SetTorque(float torque)
  {
    wheelCollider.motorTorque = torque;
  }
}
