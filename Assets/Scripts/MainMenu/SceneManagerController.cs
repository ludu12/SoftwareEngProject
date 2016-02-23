using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class SceneManagerController {

	public ISceneManagerController sceneManagerController;

	// start button
	public void OnStartButton()
	{
		sceneManagerController.LoadScene("Game");
	}

	// how to play
	public void OnHowToPlayButton()
	{
		sceneManagerController.LoadScene("Controls");
	}

	// back to menu button
	public void OnControlsBackButton()
	{
		sceneManagerController.LoadScene("MainMenu");
	}

	// exit application
	public void OnExit()
	{
		sceneManagerController.Quit();
	}

	// display menu after 3 seconds 
	public void DisplayMenu()
	{
		sceneManagerController.LoadScene("MainMenu");
	}

	public void SetSceneManager(ISceneManagerController sceneManagerController) {
		this.sceneManagerController = sceneManagerController;
	}
}
