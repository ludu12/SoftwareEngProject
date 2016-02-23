using UnityEngine;
using System.Collections;

public class CameraController {

    public Vector3 lookForwardPos;
    public Vector3 lookForwardRot;

    public Vector3 lookBackPos;
    public Vector3 lookBackRot;

    public bool fperson = false;
    public Vector3 firstPersonPos;
    public Vector3 firstPersonRot;

    public float distance = 4.8f;
    public float drop = 0.7f;

    private ICamController camController;

    public void assignStartValues(Vector3 localPosition, Quaternion localRotation)
    {

        //set lookforward
        lookForwardPos = localPosition;
        lookForwardRot = localRotation.eulerAngles;

        //set lookback
        lookBackPos = localPosition;
        lookBackPos.z = lookBackPos.z + distance;
        lookBackRot = localRotation.eulerAngles;
        lookBackRot.y = lookBackRot.y + 180;

        //set first person
        fperson = false;
        firstPersonPos = localPosition;
        firstPersonPos.z += 3.5f;
        firstPersonPos.y -= drop;
        firstPersonRot = localRotation.eulerAngles;
    }

    //alternates between first and third person view
    public void leftAltkey()
    {
        if (fperson)
        {
            fperson = false;
        }
        else {
            fperson = true;
        }
    }

    //moves position and rotation to lookBack values
    public void leftShiftkey()
    {
        camController.SetPosition(lookBackPos);
        camController.SetRotation(lookBackRot);
    }

    public void putBackCamera()
    {
        if (fperson)
        {
            camController.SetPosition(firstPersonPos);
            camController.SetRotation(firstPersonRot);
        }
        else {
            camController.SetPosition(lookForwardPos);
            camController.SetRotation(lookForwardRot);
        }
    }

    // set movement controller
    public void SetCameraController(ICamController camController)
    {
        this.camController = camController;
    }

}
