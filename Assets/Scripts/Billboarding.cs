using UnityEngine;

public class Billboarding : MonoBehaviour
{
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private bool lockYRotation;
    [SerializeField] private bool lockZRotation;
    //12 hours of work
    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;

        transform.rotation = Quaternion.Euler(
            transform.localEulerAngles.x + rotationOffset.x,
            transform.localEulerAngles.y + rotationOffset.y,
            transform.localEulerAngles.z + rotationOffset.z);
    }
}
