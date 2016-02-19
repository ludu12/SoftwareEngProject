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
        SceneManagerStub sceneManager = new SceneManagerStub();
        MainMenuManager mainMenu = new MainMenuManager(sceneManager);

        // Act
        mainMenu.OnStartButtonClick();

        // Assert
        Assert.AreEqual("Game", sceneManager.scene);
    }

    [Test]
    public void OnHowToPlayButtonClick()
    {
        // Arrange
        SceneManagerStub sceneManager = new SceneManagerStub();
        MainMenuManager mainMenu = new MainMenuManager(sceneManager);

        // Act
        mainMenu.OnHowToPlayButtonClick();

        // Assert
        Assert.AreEqual("Controls", sceneManager.scene);

    }

    [Test]
    public void OnControlsBackButtonClick()
    {
        // Arrange
        SceneManagerStub sceneManager = new SceneManagerStub();
        MainMenuManager mainMenu = new MainMenuManager(sceneManager);

        // Act
        mainMenu.OnControlsBackButtonClick();

        // Assert
        Assert.AreEqual("MainMenu", sceneManager.scene);

    }


    [Test]
    public void SplashScreenRedirect()
    {
        // Arrange
        SceneManagerStub sceneManager = new SceneManagerStub();
        SplashScreenManager splashScreen = new SplashScreenManager(sceneManager);

        // Act
        splashScreen.DisplayMenu();

        // Assert
        Assert.AreEqual("MainMenu", sceneManager.scene);

    }



    // stub class for scenemanager
    internal class SceneManagerStub : ISceneManager
    {
        public string scene;

        public void LoadScene(string scene)
        {
            this.scene = scene;
        }
    }
}
