using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Rigidbody target;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    [Range(0, 1)]
    public float smoothing = 0.5f;
    public LayerMask blocker;

    Vector3 velocity;

    void Update()
    {
        Quaternion rotation = target.rotation;
        Vector3 euler = rotation.eulerAngles;
        euler.x = euler.z = 0;
        rotation.eulerAngles = euler;

        Vector3 desiredPosition = Vector3.SmoothDamp(transform.position, target.position + rotation * new Vector3(positionOffset.x, positionOffset.y, positionOffset.z * Mathf.Sign(target.transform.InverseTransformDirection(target.velocity).z + 1)), ref velocity, smoothing);

        Debug.DrawLine(target.position, desiredPosition, Color.green);
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit, blocker))
        {
            transform.position = hit.point + (target.position - desiredPosition).normalized;
        }

        else
        {
            transform.position = desiredPosition;
        }
        transform.LookAt(target.position);
        transform.localRotation *= Quaternion.Euler(rotationOffset);
    }
}
