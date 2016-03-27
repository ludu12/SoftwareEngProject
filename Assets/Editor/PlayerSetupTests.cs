using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using NSubstitute;

[TestFixture]
[Category("PlayerSetup")]
public class PlayerSetupTests
{

    [Test]
    public void OnSetupColor()
    {
        // Arrange
        PlayerSetupContoller playerSetupController = new PlayerSetupContoller();
        var playerSetup = GetPlayerSetupMock();
        playerSetupController.SetNetworkSetup(playerSetup);
        GameObject[] goArray = new GameObject[3];
        goArray[0] = new GameObject();
        goArray[1] = new GameObject();
        goArray[2] = new GameObject();

        // Act
        playerSetupController.SetUpColor(Color.magenta, goArray); // passing in null

        // Assert
        playerSetup.Received(3).SetMaterialForGameObject(Arg.Any<GameObject>(), Arg.Any<Material>());
    }

    private IPlayerSetup GetPlayerSetupMock()
    {
        return Substitute.For<IPlayerSetup>();
    }

}
