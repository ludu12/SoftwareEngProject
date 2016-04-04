using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour, ISceneManagerController{

	public SceneManagerController sceneManager;

	private void OnEnable() {
		sceneManager = new SceneManagerController();
		sceneManager.SetSceneManager(this);
	}

	// start button
	public void OnStartButtonClick()
	{
		sceneManager.OnStartButton();
	}

	// how to play
	public void OnHowToPlayButtonClick()
	{
		sceneManager.OnHowToPlayButton();
	}

	// back to menu button
	public void OnControlsBackButtonClick()
	{
		sceneManager.OnControlsBackButton();
	}

	// exit application
	public void OnExitClick()
	{
		sceneManager.OnExit();
	}

	public void Quit(){
		Application.Quit();
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
		
}
