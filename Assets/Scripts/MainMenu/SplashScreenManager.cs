using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class SplashScreenManager : MonoBehaviour, ISceneManagerController{

    private SceneManagerController sceneManager;

    private void OnEnable()
    {
		sceneManager = new SceneManagerController();
		sceneManager.SetSceneManager(this);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("WaitAndCallDisplayMenu");
	}

    // call ienumerator for delay
    public IEnumerator WaitAndCallDisplayMenu()
	{
		yield return new WaitForSeconds (3);
        sceneManager.DisplayMenu();
	}

	public void Quit(){
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

}
