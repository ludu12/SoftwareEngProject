using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class SplashScreenManager : MonoBehaviour{

    private ISceneManager sceneManager;

    public SplashScreenManager(ISceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
    }

    private void OnEnable()
    {
        sceneManager = GetComponent<SceneManagerWrapper>();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("WaitAndCallDisplayMenu");
	}

    // call ienumerator for delay
    public IEnumerator WaitAndCallDisplayMenu()
	{
		yield return new WaitForSeconds (3);
        DisplayMenu();
	}

    // Keep this method of IEnumerator so that tests work 
    public void DisplayMenu()
    {
        sceneManager.LoadScene("MainMenu");
    }
}
