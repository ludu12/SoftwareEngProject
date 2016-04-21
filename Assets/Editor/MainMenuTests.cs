using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using NSubstitute;

[TestFixture]
[Category("MainMenu")]
public class MainMenuTests
{
    [Test]
    public void OnStartButtonClick()
    {
        // Arrange
		SceneManagerController sceneManager = new SceneManagerController();
		var manager = GetManagerMock();
		sceneManager.SetSceneManager(manager);

        // Act
        sceneManager.OnStartButton();

        // Assert
		manager.Received().LoadScene("SinglePlayerSurvival");
    }

    [Test]
    public void OnHowToPlayButtonClick()
    {
		// Arrange
		SceneManagerController sceneManager = new SceneManagerController();
		var manager = GetManagerMock();
		sceneManager.SetSceneManager(manager);

		// Act
		sceneManager.OnHowToPlayButton();

		// Assert
		manager.Received().LoadScene("Controls");

    }

    [Test]
    public void OnControlsBackButtonClick()
    {
		// Arrange
		SceneManagerController sceneManager = new SceneManagerController();
		var manager = GetManagerMock();
        manager.GetCurrentScene().Returns("SinglePlayerSurvival");
		sceneManager.SetSceneManager(manager);

		// Act
		sceneManager.OnLobbyManagerBackButton();

		// Assert
		manager.Received().LoadScene("LobbyScene");

    }


    [Test]
    public void SetUpLobbyScene()
    {
		// Arrange
		SceneManagerController sceneManager = new SceneManagerController();
		var manager = GetManagerMock();
		sceneManager.SetSceneManager(manager);

		// Act
		sceneManager.DisplayLobbyScene();

		// Assert
		manager.Received().LoadScene("LobbyScene");
    }

	[Test]
	public void OnExitClick()
	{
		// Arrange
		SceneManagerController sceneManager = new SceneManagerController();
		var manager = GetManagerMock();
		sceneManager.SetSceneManager(manager);

		// Act
		sceneManager.OnExit();

		// Assert
		manager.Received().Quit();

	}

	private ISceneManagerController GetManagerMock() {
		return Substitute.For<ISceneManagerController> ();
	}
}
