using UnityEngine;
using System.Collections;

public class CameraShift : MonoBehaviour {

    public Vector3 lookForwardPos;
    public Vector3 lookForwardRot;

    public Vector3 lookBackPos;
    public Vector3 lookBackRot;

    public bool fperson = false;
    public Vector3 firstPersonPos;
    public Vector3 firstPersonRot;

    public float distance = 4.8f;
    public float drop = 0.7f;

    // Use this for initialization
    void Start () {
        assignStartValues();
    }

    // Update is called once per frame
    void Update(){
        shiftControl();
    }

    //Initialize start values for position and movement
    public void assignStartValues(){
        distance = 4.8f;
        drop = 0.7f;

        lookForwardPos = transform.localPosition;
        lookForwardRot = transform.localRotation.eulerAngles;

        lookBackPos = transform.localPosition;
        lookBackPos.z = lookBackPos.z + distance;
        lookBackRot = transform.localRotation.eulerAngles;
        lookBackRot.y = lookBackRot.y + 180;

        fperson = false;
        firstPersonPos = transform.localPosition;
        firstPersonPos.z += 3.5f;
        firstPersonPos.y -= drop;
        firstPersonRot = transform.localRotation.eulerAngles;
    }

    //checks keybard inputs
    void shiftControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)){
            leftAltkey();
        }

        if (Input.GetKey(KeyCode.LeftShift)){
            leftShiftkey();
        }
        else {
            putBackCamera();
        }
    }

    //alternates between first and third person view
    public void leftAltkey() {
        if (fperson)
        {
            fperson = false;
        }
        else {
            fperson = true;
        }
    }

    //moves position and rotation to lookBack values
    public void leftShiftkey(){
        this.transform.localPosition = lookBackPos;
        this.transform.localRotation = Quaternion.Euler(lookBackRot);
    }

    //Stops looking backwards and resets the camera position and rotation to 1st or 3rd person
    public void putBackCamera(){
        if (fperson)
        {
            this.transform.localPosition = firstPersonPos;
            this.transform.localRotation = Quaternion.Euler(firstPersonRot);
        }
        else {
            this.transform.localPosition = lookForwardPos;
            this.transform.localRotation = Quaternion.Euler(lookForwardRot);
        }
    }
}
