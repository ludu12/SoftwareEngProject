    using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private ISceneManager sceneManager;

    public MainMenuManager(ISceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
    }

    private void OnEnable()
    {
        sceneManager = GetComponent<SceneManagerWrapper>();
    }    

    // start button
    public void OnStartButtonClick()
    {
        sceneManager.LoadScene("Game");
    }

    // how to play
	public void OnHowToPlayButtonClick()
	{
        sceneManager.LoadScene("Controls");
	}

    // back to menu button
	public void OnControlsBackButtonClick()
	{
        sceneManager.LoadScene("MainMenu");
	}

	public void OnExitClick()
	{
		Application.Quit();
	}
}
