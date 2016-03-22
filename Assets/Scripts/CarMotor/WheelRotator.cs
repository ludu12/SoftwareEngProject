using UnityEngine;
using System.Collections;

public class WheelRotator : MonoBehaviour, IRotatorController {

    public CarMotorController carController;

    // Use this for initialization
    void Start () {
        // Set the Wheel Rotator interface for the car controller to be this
        carController = gameObject.transform.parent.GetComponent<CarMotor>().carController;
        //sending gameobject name to differentiate between the wheels
        carController.SetRotatorController(this, gameObject.name);        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        StartCoroutine(updateRotate());
    }


    public IEnumerator updateRotate(){
        carController.setRotation();
        yield return null;
    }

    public void Rotate(float speed)
    {
        // Rotate around x axis relative to the speed
        float v =  speed * 42 * Time.deltaTime;
        transform.Rotate(v , 0, 0, Space.Self);
    }

}
