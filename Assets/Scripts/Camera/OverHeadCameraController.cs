using UnityEngine;
using System.Collections;
using System;

public class OverHeadCameraController : MonoBehaviour, ICamController {

    Transform target;
    public GameObject popUpDisplay;

	// Use this for initialization
	IEnumerator Start () {
        NotificationCenter.DefaultCenter().AddObserver(this, "OnPlayerDeath");

        while(target == null)
        {
            if(GameObject.FindGameObjectWithTag("Player") != null)
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            yield return null;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
            popUpDisplay.SetActive(!popUpDisplay.activeSelf);

        if (target == null)
            return;

        Vector3 newPosition = new Vector3();
        newPosition.Set(target.transform.position.x, this.transform.position.y, target.transform.position.z);
        Vector3 newRotation = this.transform.rotation.eulerAngles;
        newRotation.Set(this.transform.rotation.eulerAngles.x, target.transform.rotation.eulerAngles.y, target.transform.rotation.eulerAngles.z);
        SetPosition(newPosition);
        SetRotation(newRotation);
    }

    void OnPlayerDeath()
    {
        GetComponent<Camera>().targetTexture = null;
        popUpDisplay.SetActive(false);

        Vector3 newPosition = new Vector3();
        newPosition.Set(target.transform.position.x, 200, target.transform.position.z);
        SetPosition(newPosition);
        SetRotation(new Vector3(90,0,0));
    }

    void OnDestroy()
    {
        NotificationCenter.DefaultCenter().RemoveObserver(this, "OnPlayerDeath");
    }

    #region Interface Implementation

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
