using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;

[TestFixture]
[Category("MainMenu")]
public class MainMenuTests
{
    [Test]
    public void SimpleAddition()
    {
        //Arrange
        MainMenuManager mainMenu = new MainMenuManager();
        var sceneManager = NSubstitute.Substitute.For<SceneManager>();

        //Act
        mainMenu.OnStartButtonClick();
    }

}
