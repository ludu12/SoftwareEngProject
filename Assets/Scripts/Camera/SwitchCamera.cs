using UnityEngine;
using System.Collections;
using System;

public class SwitchCamera : MonoBehaviour, ICamController
{
    public CameraController cameraController;

    private void OnEnable()
    {
        cameraController = new CameraController();
        cameraController.SetCameraController(this);
        cameraController.assignStartValues(this.transform.localPosition, this.transform.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            cameraController.leftAltkey();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraController.leftShiftkey();
        }
        else {
            cameraController.putBackCamera();
        }
    }

    #region implementation

    public void SetPosition(Vector3 newPosition)
    {
        this.transform.localPosition = newPosition;
    }

    public void SetRotation(Vector3 eulerAngle)
    {
        this.transform.localRotation = Quaternion.Euler(eulerAngle);
    }

    #endregion
}
