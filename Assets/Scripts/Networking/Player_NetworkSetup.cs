using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour {

	[SerializeField]
	Camera carCam;

	[SerializeField]
	AudioListener audioListener;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GetComponent<CarMotor>().enabled = true;
			carCam.enabled = true;
			carCam.GetComponent<CameraShift>().enabled = true;
			audioListener.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
