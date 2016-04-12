using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Network;

public class MainMenuManager : MonoBehaviour, ISceneManagerController{

    
	private SceneManagerController sceneManager;
    public LobbyManager lobbyManager;

    void Awake()
    {
        sceneManager = new SceneManagerController();
        sceneManager.SetSceneManager(this);
        DontDestroyOnLoad(transform.gameObject);
    }

    // single player mode
    public void OnSinglePlayerSurvivalModeClick()
	{
		sceneManager.OnStartButton();
        lobbyManager.ChangeTo(null);

        Destroy(GameObject.Find("MainMenuUI(Clone)"));
        lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
        lobbyManager.topPanel.isInGame = true;
        lobbyManager.topPanel.ToggleVisibility(false);
    }

	// how to play
	public void OnHowToPlayButtonClick()
	{
		sceneManager.OnHowToPlayButton();
	}

	// back to menu button
	public void OnLobbyManagerBackButton()
	{
		sceneManager.OnLobbyManagerBackButton();
	}

	// exit application
	public void OnExitClick()
	{
		sceneManager.OnExit();
	}

    #region Interface implementation

    public void Quit(){
		Application.Quit();
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    #endregion
}
