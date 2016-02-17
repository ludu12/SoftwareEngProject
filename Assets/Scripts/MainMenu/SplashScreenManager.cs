using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("DisplayMainMenu");
	}

    IEnumerator DisplayMainMenu()
	{
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("MainMenu");
	}
}
