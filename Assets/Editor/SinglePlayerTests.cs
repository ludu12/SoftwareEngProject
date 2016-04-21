using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using NSubstitute;

[TestFixture]
[Category("SinglePlayer")]
public class SinglePlayerTests {

    [Test]
    public void ScoreIncrease()
    {
        // Arrange
        SinglePlayer singePlayer = new SinglePlayer();
        singePlayer.Score = 10;
        singePlayer.PointValue = 10;

        // Act
        singePlayer.ScoreIncrease();

        // Assert
        Assert.AreEqual(singePlayer.Score, 20);
    }

    [Test]
    public void ScoreIncreaseNewLevel()
    {
        // Arrange
        SinglePlayer singePlayer = new SinglePlayer();
        var notificationCenter = GetNotificationMock();
        singePlayer.SetNotificationInterface(notificationCenter);
        singePlayer.Score = 190;
        singePlayer.PointValue = 10;
        singePlayer.NewLevelThreshold = 200;

        // Act
        singePlayer.ScoreIncrease();

        // Assert
        notificationCenter.Received(1).PostNotification("IncreaseLevel");
    }

    [Test]
    public void OnTimer()
    {
        // Arrange
        SinglePlayer singePlayer = new SinglePlayer();

        // Act
        string result = singePlayer.OnTimer(10f);

        // Assert
        Assert.AreEqual("00:10", result);
    }

    private INotificationCenter GetNotificationMock()
    {
        return Substitute.For<INotificationCenter>();
    }
}
