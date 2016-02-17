using UnityEngine;
using System.Collections;

public class CameraShift : MonoBehaviour {

    public GameObject Camera;

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
        lookForwardPos = transform.localPosition;
        lookForwardRot = transform.localRotation.eulerAngles;

        lookBackPos = transform.localPosition;
        lookBackPos.z = lookBackPos.z + distance;
        lookBackRot = transform.localRotation.eulerAngles;
        lookBackRot.y = lookBackRot.y + 180;

        firstPersonPos = transform.localPosition;
        firstPersonPos.z += 3.5f;
        firstPersonPos.y -= drop;
        firstPersonRot = transform.localRotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            if (fperson){
                fperson = false;
            }
            else {
                fperson = true;            
            }
            
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            Camera.transform.localPosition = lookBackPos;
            Camera.transform.localRotation = Quaternion.Euler(lookBackRot);
        }        
        else {
            if (fperson) {
                Camera.transform.localPosition = firstPersonPos;
                Camera.transform.localRotation = Quaternion.Euler(firstPersonRot);
            }
            else {
                Camera.transform.localPosition = lookForwardPos;
                Camera.transform.localRotation = Quaternion.Euler(lookForwardRot);
            }
        }
        
    }
}
