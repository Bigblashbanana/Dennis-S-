using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerea : MonoBehaviour
{

    public float MouseSensitiviy = 100f;
    public Transform playerBody;

    float Xrotation = 0f; 
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitiviy * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitiviy * Time.deltaTime;

        Xrotation -= MouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f); 

        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


    }
}
