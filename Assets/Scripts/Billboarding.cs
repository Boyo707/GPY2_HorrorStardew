using Unity.VisualScripting;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;

    private Quaternion newRotation;

    Vector3 cameraDir;

    private void Start()
    {
        Quaternion cameraRot = Camera.main.transform.rotation;


        //Debug.Log(cameraRot);

        float x = cameraRot.x + rotationOffset.x;
        float y = cameraRot.y + rotationOffset.y;
        float z = cameraRot.z + rotationOffset.z;


        newRotation = new Quaternion(x, y, z, cameraRot.w);

        transform.rotation = newRotation;
    }
}
