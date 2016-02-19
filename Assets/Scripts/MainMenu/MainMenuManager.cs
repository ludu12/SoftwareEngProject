    using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

	public void OnHowToPlayButtonClick()
	{
		SceneManager.LoadScene("Controls");
	}

	public void OnControlsBackButtonClick()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void OnExitClick()
	{
		Application.Quit();
	}
}
