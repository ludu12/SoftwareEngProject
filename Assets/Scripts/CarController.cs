using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour 
{
    float speed = 0f;
    float maxSpeed = 20f;
    float accel = .2f;
	void Update () 
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            StartCoroutine(OnForward());
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            StartCoroutine(OnRight());
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            StartCoroutine(OnDown());
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            StartCoroutine(OnLeft());
	}
	
    IEnumerator OnForward()
    {
        speed += accel;
        if (speed > maxSpeed)
            speed = maxSpeed;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        yield return null;
    }
    IEnumerator OnRight()
    {
       
        yield return null;
    }
    IEnumerator OnLeft()
    {
     
        yield return null;
    }
    IEnumerator OnDown()
    {
       
        yield return null;
    }
}
