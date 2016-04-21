using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
[Category("Networking")]
public class PlayerSyncTests : MonoBehaviour {

    [Test]
    public void HasMovedFalse()
    {
        // Arrange
        PlayerSync playerSync = new PlayerSync();
        var playerSyncInterface = GetPlayerSyncMock();
        playerSync.SetPlayerSync(playerSyncInterface);
        Vector3 lastPos = new Vector3(1, 1, 1);
        Quaternion lastRot = new Quaternion(1, 1, 1, 1);
        playerSync.SetLastPositionRotation(lastPos, lastRot);

        // Act
        bool result = playerSync.HasMoved(lastPos, lastRot);

        // Assert
        Assert.AreEqual(false, result);
    }

    [Test]
    public void HasMovedPosTrue()
    {
        // Arrange
        PlayerSync playerSync = new PlayerSync();
        var playerSyncInterface = GetPlayerSyncMock();
        playerSync.SetPlayerSync(playerSyncInterface);
        Vector3 lastPos = new Vector3(0, 0, 0);
        Quaternion lastRot = new Quaternion(1, 1, 1, 1);
        playerSync.SetLastPositionRotation(lastPos, lastRot);

        // Act
        bool result = playerSync.HasMoved(new Vector3(1,1,1), lastRot);

        // Assert
        Assert.AreEqual(true, result);
    }

    [Test]
    public void HasMovedRotTrue()
    {
        // Arrange
        PlayerSync playerSync = new PlayerSync();
        var playerSyncInterface = GetPlayerSyncMock();
        playerSync.SetPlayerSync(playerSyncInterface);
        Vector3 lastPos = new Vector3(1, 1, 1);
        Quaternion lastRot = new Quaternion(0, 0, 0, 0);
        playerSync.SetLastPositionRotation(lastPos, lastRot);

        // Act
        bool result = playerSync.HasMoved(lastPos, new Quaternion(1, 1, 1, 1));

        // Assert
        Assert.AreEqual(true, result);
    }

    [Test]
    public void CallLerpPosition()
    {
        // Arrange
        PlayerSync playerSync = new PlayerSync();
        var playerSyncInterface = GetPlayerSyncMock();
        playerSync.SetPlayerSync(playerSyncInterface);
        Vector3 testVector = new Vector3(1, 1, 1);

        // Act
        playerSync.CallLerpPosition(testVector);

        // Assert
        playerSyncInterface.Received().LerpPosition(testVector, 15f);
    }

    [Test]
    public void CallLerpRotation()
    {
        // Arrange
        PlayerSync playerSync = new PlayerSync();
        var playerSyncInterface = GetPlayerSyncMock();
        playerSync.SetPlayerSync(playerSyncInterface);
        Quaternion testRot = new Quaternion(0, 0, 0, 0);

        // Act
        playerSync.CallLerpRotation(testRot);

        // Assert
        playerSyncInterface.Received().LerpRotation(testRot, 15f);
    }

    private IPlayerSync GetPlayerSyncMock()
    {
        return Substitute.For<IPlayerSync>();
    }
}
