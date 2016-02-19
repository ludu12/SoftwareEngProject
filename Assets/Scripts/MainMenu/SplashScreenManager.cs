using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class SplashScreenManager : MonoBehaviour, ISceneManager{

    public MainMenuController controller;

    private void OnEnable()
    {
        controller = GetComponent<MainMenuController>();
        controller.SetSceneManangerController(this);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("DisplayMainMenu");
	}

    IEnumerator DisplayMainMenu()
	{
		yield return new WaitForSeconds (3);
		controller.LoadScene ("MainMenu");
	}

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
