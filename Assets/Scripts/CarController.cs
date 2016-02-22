using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour 
{
    //Car speed
    public float speed = 0f;
    bool isDriving = false;
    //Car limits
    public float maxSpeed = 15f;
    public float maxReverseSpeed = -10f;
    public float accel = 0.1f;
    public float decel = 0.15f;
    public float rotationSpeed = 45f;
	
    void FixedUpdate () 
    {
        //Keyboard inputs for controlling car
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            StartCoroutine(OnForward());
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            StartCoroutine(OnDown());
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            StartCoroutine(OnRight());
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            StartCoroutine(OnLeft());
        if (!isDriving)
            StartCoroutine(OnSlowDown());

        isDriving = false;
    }

    public IEnumerator OnForward()
    {
        isDriving = true;
        // call logic
        GetForwardSpeed();

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        yield return null;
    }

    public IEnumerator OnRight()
    {
        Vector3 turnVector = new Vector3(0, 1f, 0) * rotationSpeed * Time.deltaTime;

        if(speed != 0)
            transform.Rotate(turnVector);

        yield return null;
    }
    public IEnumerator OnLeft()
    {
        Vector3 turnVector = new Vector3(0, -1f, 0) * rotationSpeed * Time.deltaTime;

        if(speed != 0)
            transform.Rotate(turnVector);

        yield return null;
    }
    public IEnumerator OnDown()
    {
        isDriving = true;

        speed -= decel;
        if (speed > 0)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        else
        {
            if (speed < maxReverseSpeed)
                speed = maxReverseSpeed;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        yield return null;
    }

    public IEnumerator OnSlowDown()
    {
        if (speed < 0)
        {
            speed += decel;
            if (speed > 0)
                speed = 0;

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            speed -= decel;
            if (speed < 0)
                speed = 0;

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        yield return null;
    }

    // Logic for getting speed of car going forward
    public void GetForwardSpeed()
    {
        speed += accel;
        if (speed > maxSpeed)
            speed = maxSpeed;
    }
}
