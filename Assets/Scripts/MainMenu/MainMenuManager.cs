using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour, ISceneManager {

    public MainMenuController controller;

    private void OnEnable()
    {
        controller = GetComponent<MainMenuController>();
        controller.SetSceneManangerController(this);
    }    

    // start button
    public void OnStartButtonClick()
    {
        controller.LoadScene("Game");
    }

    // how to play
	public void OnHowToPlayButtonClick()
	{
        controller.LoadScene("Controls");
	}

    // back to menu button
	public void OnControlsBackButtonClick()
	{
        controller.LoadScene("MainMenu");
	}

    #region ISceneManager implementation
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    #endregion
}
