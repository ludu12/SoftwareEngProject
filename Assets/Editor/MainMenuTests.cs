using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;

[TestFixture]
[Category("MainMenu")]
public class MainMenuTests
{
    [Test]
    public void OnStartButtonClick()
    {
        // Arrange
        var sceneManagerStub = GetSceneManager();
        var mainMenuManger = GetControllerMock(sceneManagerStub);


        // Act
        //mainMenu.OnStartButtonClick();

        // Assert
        //Assert.That("Game" == sceneManagerStub.scene);
    }

    [Test]
    public void OnHowToPlayButtonClick()
    {

    }

    [Test]
    public void OnControlsBackButtonClick()
    {

    }

    private ISceneManager GetSceneManager()
    {
        return NSubstitute.Substitute.For<ISceneManager>();
    }

    private MainMenuController GetControllerMock(ISceneManager sceneManager)
    {
        var mainMenuManager = NSubstitute.Substitute.For<MainMenuController>();
        mainMenuManager.SetSceneManangerController(sceneManager);
        return mainMenuManager;
    }
}
