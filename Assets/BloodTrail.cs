using UnityEngine;

public class BloodTrail : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplashPref;
    [SerializeField] private float splashDuration;
    [SerializeField] private int maxBloodSplashes;

    private GameObject[] bloodSplashes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bloodSplashes = new GameObject[maxBloodSplashes];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
