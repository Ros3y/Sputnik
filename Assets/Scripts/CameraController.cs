using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 CameraOffset;

    public float RotationSpeed = 5.0f;

    public bool RotateAroundPlayer = true;
    void Start()
    {
        CameraOffset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(RotateAroundPlayer)
        {
        Quaternion cameraAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
        CameraOffset = cameraAngle * CameraOffset;
        }

        transform.position = Player.transform.position + CameraOffset;
        transform.LookAt(Player.transform);


    }
}
