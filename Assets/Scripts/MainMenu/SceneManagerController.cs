using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class SceneManagerController {

	public ISceneManagerController sceneManagerController;

	// start button
	public void OnStartButton()
	{
		sceneManagerController.LoadScene("SinglePlayerSurvival");
	}

	// how to play
	public void OnHowToPlayButton()
	{
		sceneManagerController.LoadScene("Controls");
	}

	// back to menu button
	public void OnLobbyManagerBackButton()
	{
        Debug.Log(sceneManagerController.GetCurrentScene());

        if (sceneManagerController.GetCurrentScene() == "SinglePlayerSurvival")
		    sceneManagerController.LoadScene("LobbyScene");
	}

	// exit application
	public void OnExit()
	{
		sceneManagerController.Quit();
	}

	// display menu after 3 seconds 
	public void DisplayLobbyScene()
	{
		sceneManagerController.LoadScene("LobbyScene");
	}

	public void SetSceneManager(ISceneManagerController sceneManagerController) {
		this.sceneManagerController = sceneManagerController;
	}
}
