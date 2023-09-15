using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    private float mainCameraXRotation = 0;
    private float mouseX;
    private float mouseY;
    private float mouseSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseInput();
        TurnCamera();
    }
    
    // Check Mouse Input
    void CheckMouseInput()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    void TurnCamera()
    {
        if (mouseX != 0)
        {
            transform.Rotate(Vector3.up * mouseX * mouseSpeed * Time.deltaTime);
        }

        if (mouseY != 0)
        {
            // Prevent reverse mouse
            mainCameraXRotation -= mouseY;
            // Prevent player look up or look down above 90 deg
            mainCameraXRotation = Mathf.Clamp(mainCameraXRotation, -90, 90);
            mainCamera.transform.localRotation = Quaternion.Euler(mainCameraXRotation, 0, 0);
        }
    }
}
