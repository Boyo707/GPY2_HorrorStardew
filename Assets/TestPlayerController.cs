using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    public float steps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * steps;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * steps;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * steps;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * steps;
        }
    }
}
