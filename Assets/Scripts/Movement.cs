using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    public Vector3 maxSpeed;

    public GameObject Camera;

    public GameObject player;

    public float RotationSpeed;
    private float AngleX;
    private float AngleY;


    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if(maxSpeed.z != GetComponent<Rigidbody>().velocity.z)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0, 0, 0.025f);
            }
        }

        if(Input.GetKey(KeyCode.A))
        {
            if(maxSpeed.z != GetComponent<Rigidbody>().velocity.x)
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(0.025f, 0, 0);
            }    
        }

        if(Input.GetKey(KeyCode.S))
        {
            if(maxSpeed.z != -(GetComponent<Rigidbody>().velocity.z))
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(0, 0, 0.025f);
            } 
        }

        if(Input.GetKey(KeyCode.D))
        {
            if(maxSpeed.z != -(GetComponent<Rigidbody>().velocity.x))
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0.025f, 0, 0);
            } 
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity += new Vector3(0, 10, 0); 
        }

        RotateCharacter();    
    }

    void RotateCharacter()
    {
        AngleX += Input.GetAxis("Mouse X") * RotationSpeed * -Time.deltaTime;
        AngleY += Input.GetAxis("Mouse Y") * RotationSpeed * -Time.deltaTime;
        player.transform.localRotation = Quaternion.AngleAxis(AngleX, Vector3.up); 
        player.transform.localRotation = Quaternion.AngleAxis(AngleY, Vector3.right);    
    }
}
