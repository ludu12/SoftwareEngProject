using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Reflection;
using System.Collections.Generic;

[TestFixture]
[Category("Map")]
public class MapGenerationTests {

    [Test]
    public void StartBeginningPieceIsGround()
    {
        //Arrange
        MapGenerationScript mapGeneration = new MapGenerationScript();
        var map = GetMapMock();
        mapGeneration.SetMapInterface(map);
        GameObject ground = new GameObject();
        mapGeneration.Ground = ground;

        //Act
        mapGeneration.Start(new GameObject());

        //Assert
        map.Received().InstantiateGameObject(ground, new Vector3(0, 0, 0), Quaternion.identity);
    }

    [Test]
    public void StartGeneratedFourteenPieces()
    {
        //Arrange
        MapGenerationScript mapGeneration = new MapGenerationScript();
        var map = GetMapMock();
        mapGeneration.SetMapInterface(map);
        SetGameObjectPrefabs(mapGeneration);

        //Act
        mapGeneration.Start(new GameObject());

        //Assert
        map.Received(14).InstantiateGameObject(Arg.Any<GameObject>(), Arg.Any<Vector3>(), Arg.Any<Quaternion>()); // exactly 15 times
        map.DidNotReceive().DestroyThis(Arg.Any<GameObject>()); // none of which were destroy calls
    }

    [Test]
    public void StartMapQueueCountIsFifteen()
    {
        //Arrange
        MapGenerationScript mapGeneration = new MapGenerationScript();
        var map = GetMapMock();
        mapGeneration.SetMapInterface(map);

        //Act
        mapGeneration.Start(new GameObject());
        Queue<GameObject> queue = (Queue<GameObject>)GetInstanceField(typeof(MapGenerationScript), mapGeneration, "mapQueue");

        //Assert
        Assert.AreEqual(queue.Count, 15);
    }

    [Test]
    public void GenerateMapPiece()
    {
        //Arrange
        MapGenerationScript mapGeneration = new MapGenerationScript();
        var map = GetMapMock();
        mapGeneration.SetMapInterface(map);
        mapGeneration.Start(new GameObject());
        map.ClearReceivedCalls();

        //Act
        mapGeneration.GenerateMapPiece();

        //Assert       
        map.Received(1).InstantiateGameObject(Arg.Any<GameObject>(), Arg.Any<Vector3>(), Arg.Any<Quaternion>()); // received only 2 calls, one is instantiate
        map.Received(1).DestroyThis(Arg.Any<GameObject>()); // one was a destroy call
    }

    [Test]
    public void GenerateMapPieceDestroysLastPiece()
    {
        //Arrange
        MapGenerationScript mapGeneration = new MapGenerationScript();
        var map = GetMapMock();
        mapGeneration.SetMapInterface(map);
        mapGeneration.Start(new GameObject());
        Queue<GameObject> queue = (Queue<GameObject>)GetInstanceField(typeof(MapGenerationScript), mapGeneration, "mapQueue");

        //Act
        mapGeneration.GenerateMapPiece();

        //Assert
        map.Received().DestroyThis(queue.Dequeue()); // destroyed last map queue piece
    }

    private void SetGameObjectPrefabs(MapGenerationScript mapGeneration)
    {
        mapGeneration.Ground = new GameObject();
        mapGeneration.GroundT = new GameObject();
        mapGeneration.BridgeF = new GameObject();
        mapGeneration.BridgeT = new GameObject();
        mapGeneration.BridgeU = new GameObject();
    }

    private IDestroyInstantiate GetMapMock()
    {
        return Substitute.For<IDestroyInstantiate>();
    }

    /// <summary>
    /// Uses reflection to get the field value from an object.
    /// </summary>
    ///
    /// <param name="type">The instance type.</param>
    /// <param name="instance">The instance object.</param>
    /// <param name="fieldName">The field's name which is to be fetched.</param>
    ///
    /// <returns>The field value from the object.</returns>
    internal static object GetInstanceField(Type type, object instance, string fieldName)
    {
        BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            | BindingFlags.Static;
        FieldInfo field = type.GetField(fieldName, bindFlags);
        return field.GetValue(instance);
    }
}
