 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static class Constants
{
    public const int maxLength = 20;
}

public class MapGenerationScript {

    char[,,] map = new char[Constants.maxLength, Constants.maxLength, 2];
    /*
    * position does x, y, direction 0 equals forward, 1 equals right, 2 equals back, 3 left
    * 0 = x
    * 1 = y
    * 2 = direction
    * 3 = count flag for first loop
    * 4 = level
    * 5 = last move = c flag
    */
    int[] position = { 9, 19, 0, 0, 0, 0 };
    int[,] queue = new int[15, 3];

    int i;
    int j;
    int k;
    int sentinel = 0;

    public GameObject Ground;
    public GameObject GroundT;
    public GameObject BridgeU;
    public GameObject BridgeT;
    public GameObject BridgeF;

    IDestroyInstantiate mapInterface;
    INotificationCenter notificationCenter;

    private char[,] myMap = new char[15, 2];
    private char step;
    int direction;
    private Vector3 nextLocation;
    Queue<GameObject> mapQueue;
    GameObject piece;
    double bridgeSpawnHeight = 3.6;


    /// <summary>
    /// Set the interface for this humble object
    /// </summary>
    /// <param name="mapInterface"></param>
    public void SetMapInterface(IDestroyInstantiate mapInterface)
    {
        this.mapInterface = mapInterface;
    }

    /// <summary>
    /// Set the interface for this humble object
    /// </summary>
    /// <param name="mapInterface"></param>
    public void SetNotificationInterface(INotificationCenter notificationCenter)
    {
        this.notificationCenter = notificationCenter;
    }

    /// <summary>
    /// Call this method in the mono script to initialize the map, then parse through and instantiate the prefabs
    /// </summary>
    public void Start(GameObject startPiece)
    {
        nextLocation = new Vector3(0, 0, 0);
        mapQueue = new Queue<GameObject>();
        myMap = initialize();

        mapQueue.Enqueue(startPiece);
        nextLocation.z += 20;

        direction = myMap[0, 1];
        //myMap[1, 1] = '0';
        for (int i = 1; i < 15; i++)
        {
            if (myMap[i, 0].Equals('S'))
            {
                if (nextLocation.y == 0)
                {
                    if (myMap[i, 1].Equals('0'))
                    {
                        piece = mapInterface.InstantiateGameObject(Ground, nextLocation, Quaternion.Euler(0, 0, 0));
                        nextLocation.z += 20;
                    }
                    else if (myMap[i, 1].Equals('1'))
                    {
                        piece = mapInterface.InstantiateGameObject(Ground, nextLocation, Quaternion.Euler(0, 90, 0));
                        nextLocation.x += 20;
                    }
                    else if (myMap[i, 1].Equals('2'))
                    {
                        piece = mapInterface.InstantiateGameObject(Ground, nextLocation, Quaternion.Euler(0, 0, 0));
                        nextLocation.z -= 20;
                    }
                    else if (myMap[i, 1].Equals('3'))
                    {
                        piece = mapInterface.InstantiateGameObject(Ground, nextLocation, Quaternion.Euler(0, 90, 0));
                        nextLocation.x -= 20;
                    }
                }
                else
                {
                    if (myMap[i, 1].Equals('0'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeF, nextLocation, Quaternion.Euler(0, 0, 0));
                        nextLocation.z += 20;
                    }
                    else if (myMap[i, 1].Equals('1'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeF,nextLocation, Quaternion.Euler(0, 90, 0));
                        nextLocation.x += 20;
                    }
                    else if (myMap[i, 1].Equals('2'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeF, nextLocation, Quaternion.Euler(0, 0, 0));
                        nextLocation.z -= 20;
                    }
                    else if (myMap[i, 1].Equals('3'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeF, nextLocation, Quaternion.Euler(0, 90, 0));
                        nextLocation.x -= 20;
                    }
                }
            }
            else if (myMap[i, 0].Equals('C'))
            {
                if (nextLocation.y == 0)
                {
                    nextLocation.y = (float)bridgeSpawnHeight;
                    if (myMap[i, 1].Equals('0'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 180, 0));
                        nextLocation.z += 20;
                    }
                    else if (myMap[i, 1].Equals('1'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 270, 0));
                        nextLocation.x += 20;
                    }
                    else if (myMap[i, 1].Equals('2'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 0, 0));
                        nextLocation.z -= 20;
                    }
                    else if (myMap[i, 1].Equals('3'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 90, 0));
                        nextLocation.x -= 20;
                    }
                    nextLocation.y = (float)bridgeSpawnHeight * 2;
                }
                else
                {
                    nextLocation.y = (float)bridgeSpawnHeight;
                    if (myMap[i, 1].Equals('0'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 0, 0));
                        nextLocation.z += 20;
                    }
                    else if (myMap[i, 1].Equals('1'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 90, 0));
                        nextLocation.x += 20;
                    }
                    else if (myMap[i, 1].Equals('2'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 180, 0));
                        nextLocation.z -= 20;
                    }
                    else if (myMap[i, 1].Equals('3'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 270, 0));
                        nextLocation.x -= 20;
                    }
                    nextLocation.y = 0;
                }
            }
            else if (myMap[i, 0].Equals('R'))
            {
                if (nextLocation.y == 0)
                {
                    if (myMap[i, 1].Equals('0'))
                    {
                        piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 270, 0));
                        nextLocation.z += 20;
                    }
                    else if (myMap[i, 1].Equals('1'))
                    {
                        piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 0, 0));
                        nextLocation.x += 20;
                    }
                    else if (myMap[i, 1].Equals('2'))
                    {
                        piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 90, 0));
                        nextLocation.z -= 20;
                    }
                    else if (myMap[i, 1].Equals('3'))
                    {
                        piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 180, 0));
                        nextLocation.x -= 20;
                    }
                }
                else
                {
                    if (myMap[i, 1].Equals('0'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 270, 0));
                        nextLocation.z += 20;
                    }
                    else if (myMap[i, 1].Equals('1'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 0, 0));
                        nextLocation.x += 20;
                    }
                    else if (myMap[i, 1].Equals('2'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 90, 0));
                        nextLocation.z -= 20;
                    }
                    else if (myMap[i, 1].Equals('3'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 180, 0));
                        nextLocation.x -= 20;
                    }
                }
                direction = myMap[i, 1];
            }
            else if (myMap[i, 0].Equals('L'))
            {
                if (nextLocation.y == 0)
                {
                    if (myMap[i, 1].Equals('0'))
                    {
                        piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 180, 0));
                        nextLocation.z += 20;
                    }
                    else if (myMap[i, 1].Equals('1'))
                    {
                        piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 270, 0));
                        nextLocation.x += 20;
                    }
                    else if (myMap[i, 1].Equals('2'))
                    {
                        piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 0, 0));
                        nextLocation.z -= 20;
                    }
                    else if (myMap[i, 1].Equals('3'))
                    {
                        piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 90, 0));
                        nextLocation.x -= 20;
                    }
                }
                else
                {
                    if (myMap[i, 1].Equals('0'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 180, 0));
                        nextLocation.z += 20;
                    }
                    else if (myMap[i, 1].Equals('1'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 270, 0));
                        nextLocation.x += 20;
                    }
                    else if (myMap[i, 1].Equals('2'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 0, 0));
                        nextLocation.z -= 20;
                    }
                    else if (myMap[i, 1].Equals('3'))
                    {
                        piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 90, 0));
                        nextLocation.x -= 20;
                    }
                }
                direction = myMap[i, 1];
            }
            mapQueue.Enqueue(piece);
        }
    }

    /// <summary>
    /// This method will be called from the mono script, either from a collider notification or through invoke repeating call
    /// </summary>
    public void GenerateMapPiece()
    {
        step = mapStep();
        direction = getDirection();

        if (step.Equals('S'))
        {
            if (nextLocation.y == 0)
            {
                if (direction == 0)
                {
                    piece = mapInterface.InstantiateGameObject(Ground, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = mapInterface.InstantiateGameObject(Ground, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = mapInterface.InstantiateGameObject(Ground, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = mapInterface.InstantiateGameObject(Ground, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x -= 20;
                }
            }
            else
            {
                if (direction == 0)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeF, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeF, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeF, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeF, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x -= 20;
                }
            }

        }
        else if (step.Equals('C'))
        {
            if (nextLocation.y == 0)
            {
                nextLocation.y = (float)bridgeSpawnHeight;
                if (direction == 0)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 180, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 270, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 0, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 90, 0));
                    nextLocation.x -= 20;
                }
                nextLocation.y = (float)bridgeSpawnHeight * 2;
            }
            else
            {
                nextLocation.y = (float)bridgeSpawnHeight;
                if (direction == 0)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 0, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 90, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 180, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeU, nextLocation, Quaternion.Euler(20, 270, 0));
                    nextLocation.x -= 20;
                }
                nextLocation.y = 0;
            }
        }
        else if (step.Equals('R'))
        {
            if (nextLocation.y == 0)
            {
                if (direction == 0)
                {
                    piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 270, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 180, 0));
                    nextLocation.x -= 20;
                }
            }
            else
            {
                if (direction == 0)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 270, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 180, 0));
                    nextLocation.x -= 20;
                }
            }
        }
        else if (step.Equals('L'))
        {
            if (nextLocation.y == 0)
            {
                if (direction == 0)
                {
                    piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 180, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 270, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = mapInterface.InstantiateGameObject(GroundT, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x -= 20;
                }
            }
            else
            {
                if (direction == 0)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 180, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 270, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = mapInterface.InstantiateGameObject(BridgeT, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x -= 20;
                }
            }
        }
        mapQueue.Enqueue(piece);
        mapInterface.DestroyThis(mapQueue.Dequeue());
    }

    /// <summary>
    /// Gets the index of the position of this piece
    /// </summary>
    /// <param name="piece"></param>
    /// <returns></returns>
    public bool isFrontPiece(GameObject piece)
    {
        Debug.Log("In map script, piece index: " + mapQueue.ToArray().ToList().IndexOf(piece));
        if (mapQueue.ToArray().ToList().IndexOf(piece) > 10)
            return true;
        else
            return false;
    }

    int getDirection()
    {
        return (position[2]);
    }

    char[,] initialize()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        int i;
        int j;
        int k;
        char[,] move = new char[15,2];

	    for(k = 0; k< 2; k++){
		    for(j = 0; j<Constants.maxLength; j++){
			    for(i = 0; i< Constants.maxLength; i++){
				    map[i,j,k] = '*';
			    }
		    }
	    }
        
        move[0,0] = 'S';
        move[0, 1] = position[2].ToString()[0];
        straight(map, position, queue, sentinel);
        sentinel = 1;
        
        for(int z = 1; z < 15; z++)
        { 
		    move[z,0] = mapStep();
            move[z,1] = position[2].ToString()[0];
        }
        return (move);
    }

    char mapStep()
    {
        int nextVal = 0;
        char step = '*';
        while (step == '*')
        {
            if ((leftPossible(map, position)==1) && (straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && (rightPossible(map, position)==1))
            {
                nextVal = Random.Range(0,6);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                    case 4:
                        nextVal = 1;
                        break;
                    case 5:
                        nextVal = 2;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 5);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                    case 4:
                        nextVal = 1;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 5);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                    case 4:
                        nextVal = 2;
                        break;
                }
            }
            else if (!(leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(1, 6);
                switch (nextVal)
                {
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                    case 4:
                        nextVal = 1;
                        break;
                    case 5:
                        nextVal = 6;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 3);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 1;
                        break;
                    case 2:
                        nextVal = 2;
                        break;
                }
            }
            else if (!(leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 2);
                switch (nextVal)
                {
                    case 0:
                        nextVal = 1;
                        break;
                    case 1:
                        nextVal = 2;
                        break;
                }
            }
            else if (!(leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 4);
                switch (nextVal)
                {
                    case 0:
                        nextVal = 0;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 2;
                        break;
                }
            }
            else if (!(leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 4);
                switch (nextVal)
                {
                    case 0:
                        nextVal = 0;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 1;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 2);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 2;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 2);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 1;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = Random.Range(0, 4);
                switch (nextVal)
                {
                    case 0:
                        nextVal = -1;
                        break;
                    case 1:
                        nextVal = 0;
                        break;
                    case 2:
                        nextVal = 0;
                        break;
                    case 3:
                        nextVal = 0;
                        break;
                }
            }
            else if ((leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = -1;
            }
            else if (!(leftPossible(map, position) == 1) && (straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = 0;
            }
            else if (!(leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && (levelChangePossible(map, position) == 1) && !(rightPossible(map, position) == 1))
            {
                nextVal = 1;
            }
            else if (!(leftPossible(map, position) == 1) && !(straightPossible(map, position) == 1) && !(levelChangePossible(map, position) == 1) && (rightPossible(map, position) == 1))
            {
                nextVal = 2;
            }
            switch (nextVal)
            {
                case -1:
                    if (leftPossible(map, position)==1)
                    {
                        if (leftLeft(map, position, queue, sentinel) >= 15)
                        {
                            left(map, position, queue, sentinel);
                            if (sentinel < 14)
                            {
                                sentinel++;
                            }
                            step = 'L';
                            position[5] = 0;
                        }
                    }
                    break;
                case 0:
                    if (straightPossible(map, position)==1)
                    {
                        if (straightLeft(map, position, queue, sentinel) >= 15)
                        {
                            straight(map, position, queue, sentinel);
                            if (sentinel < 14)
                            {
                                sentinel++;
                            }
                            step = 'S';
                            position[5] = 0;
                        }
                    }
                    break;
                case 1:
                    if (levelChangePossible(map, position) == 1)
                    {
                        if (levelChangeLeft(map, position, queue, sentinel) >= 15)
                        {
                            levelChange(map, position, queue, sentinel);
                            if (sentinel < 14)
                            {
                                sentinel++;
                            }
                            step = 'C';
                            position[5] = 1;
                        }
                    }
                    break;
                case 2:
                    if (rightPossible(map, position)==1)
                    {
                        if (rightLeft(map, position, queue, sentinel) >= 15)
                        {
                            right(map, position, queue, sentinel);
                            if (sentinel < 14)
                            {
                                sentinel++;
                            }
                            step = 'R';
                            position[5] = 0;
                        }
                    }
                    break;
            }
        }
        return (step);
    }

    void straight(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        if (sentinel == 14)
        {
            if (position[3] <= 13)
            {
                if (map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]].Equals('C'))
                {
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], 0] = '*';
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], 1] = '*';
                }
                else
                {
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]] = '*';

                }
            }
            else if (position[3] > 13)
            {
                if (map[queue[0, 0], queue[0, 1], queue[0, 2]].Equals('C'))
                {
                    map[queue[0, 0], queue[0, 1], 0] = '*';
                    map[queue[0, 0], queue[0, 1], 1] = '*';
                }
                else
                {
                    map[queue[0, 0], queue[0, 1], queue[0, 2]] = '*';
                }
            }
        }

        switch (position[2])
        {
            case 0:
                position[1] = position[1] - 1;
                break;
            case 1:
                position[0] = position[0] + 1;
                break;
            case 2:
                position[1] = position[1] + 1;
                break;
            case 3:
                position[0] = position[0] - 1;
                break;
        }
        queue[position[3],0] = position[0];
        queue[position[3],1] = position[1];
        queue[position[3],2] = position[4];
        if (position[3] > 13)
        {
            position[3] = 0;
        }
        else {
            position[3]++;
        }
        map[position[0],position[1],position[4]] = 'S';
    }

    void levelChange(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        if (sentinel == 14)
        {
            if (position[3] <= 13)
            {
                if (map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]].Equals('C'))
                {
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], 0] = '*';
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], 1] = '*';
                }
                else
                {
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]] = '*';

                }
            }
            else if (position[3] > 13)
            {
                if (map[queue[0, 0], queue[0, 1], queue[0, 2]].Equals('C'))
                {
                    map[queue[0, 0], queue[0, 1], 0] = '*';
                    map[queue[0, 0], queue[0, 1], 1] = '*';
                }
                else
                {
                    map[queue[0, 0], queue[0, 1], queue[0, 2]] = '*';
                }
            }
        }

        switch (position[2])
        {
            case 0:
                position[1] = position[1] - 1;
                break;
            case 1:
                position[0] = position[0] + 1;
                break;
            case 2:
                position[1] = position[1] + 1;
                break;
            case 3:
                position[0] = position[0] - 1;
                break;
        }
        switch (position[4])
        {
            case 0:
                position[4] = 1;
                break;
            case 1:
                position[4] = 0;
                break;
        }
        queue[position[3], 0] = position[0];
        queue[position[3], 1] = position[1];
        queue[position[3], 2] = position[4];
        if (position[3] > 13)
        {
            position[3] = 0;
        }
        else {
            position[3]++;
        }
        map[position[0], position[1], 0] = 'C';
        map[position[0], position[1], 1] = 'C';
    }

    void left(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        if (sentinel == 14)
        {
            if (position[3] <= 13)
            {
                if (map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]].Equals('C'))
                {
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], 0] = '*';
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], 1] = '*';
                }
                else
                {
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]] = '*';

                }
            }
            else if (position[3] > 13)
            {
                if (map[queue[0, 0], queue[0, 1], queue[0, 2]].Equals('C'))
                {
                    map[queue[0, 0], queue[0, 1], 0] = '*';
                    map[queue[0, 0], queue[0, 1], 1] = '*';
                }
                else
                {
                    map[queue[0, 0], queue[0, 1], queue[0, 2]] = '*';
                }
            }
        }

        switch (position[2])
        {
            case 0:
                position[1] = position[1] - 1;
                position[2] = 3;
                break;
            case 1:
                position[0] = position[0] + 1;
                position[2] = 0;
                break;
            case 2:
                position[1] = position[1] + 1;
                position[2] = 1;
                break;
            case 3:
                position[0] = position[0] - 1;
                position[2] = 2;
                break;
        }
        queue[position[3],0] = position[0];
        queue[position[3],1] = position[1];
        queue[position[3], 2] = position[4];
        if (position[3] > 13)
        {
            position[3] = 0;
        }
        else {
            position[3]++;
        }
        map[position[0],position[1],position[4]] = 'L';
    }

    void right(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        if (sentinel == 14)
        {
            if (position[3] <= 13)
            {
                if (map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]].Equals('C'))
                {
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], 0] = '*';
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], 1] = '*';
                }
                else
                {
                    map[queue[position[3] + 1, 0], queue[position[3] + 1, 1], queue[position[3] + 1, 2]] = '*';

                }
            }
            else if (position[3] > 13)
            {
                if (map[queue[0, 0], queue[0, 1], queue[0, 2]].Equals('C'))
                {
                    map[queue[0, 0], queue[0, 1], 0] = '*';
                    map[queue[0, 0], queue[0, 1], 1] = '*';
                }
                else
                {
                    map[queue[0, 0], queue[0, 1], queue[0, 2]] = '*';
                }
            }
        }

        switch (position[2])
        {
            case 0:
                position[1] = position[1] - 1;
                position[2] = 1;
                break;
            case 1:
                position[0] = position[0] + 1;
                position[2] = 2;
                break;
            case 2:
                position[1] = position[1] + 1;
                position[2] = 3;
                break;
            case 3:
                position[0] = position[0] - 1;
                position[2] = 0;
                break;
        }
        queue[position[3],0] = position[0];
        queue[position[3],1] = position[1];
        queue[position[3], 2] = position[4];
        if (position[3] > 13)
        {
            position[3] = 0;
        }
        else {
            position[3]++;
        }
        map[position[0],position[1],position[4]] = 'R';
    }

    int leftPossible(char[,,] map, int[] position)
    {
        int flag = 0;
        switch (position[2])
        {
            case 0:
                if (position[0] != 0)
                {
                    if ((map[position[0],position[1] - 1, position[4]] == '*') && (map[position[0] - 1,position[1] - 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 1:
                if (position[1] != 0)
                {
                    if ((map[position[0] + 1,position[1], position[4]] == '*') && (map[position[0] + 1,position[1] - 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 2:
                if (position[0] != Constants.maxLength - 1)
                {
                    if ((map[position[0],position[1] + 1, position[4]] == '*') && (map[position[0] + 1,position[1] + 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 3:
                if (position[1] != Constants.maxLength - 1)
                {
                    if ((map[position[0] - 1,position[1], position[4]] == '*') && (map[position[0] - 1,position[1] + 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
        }
        return flag;
    }

    int straightPossible(char[,,] map, int[] position)
    {
        int flag = 0;
        switch (position[2])
        {
            case 0:
                if (position[1] != 1)
                {
                    if ((map[position[0],position[1] - 1, position[4]] == '*') && (map[position[0],position[1] - 2, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 1:
                if (position[0] != Constants.maxLength - 2)
                {
                    if ((map[position[0] + 1,position[1], position[4]] == '*') && (map[position[0] + 2,position[1], position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 2:
                if (position[1] != Constants.maxLength - 2)
                {
                    if ((map[position[0],position[1] + 1, position[4]] == '*') && (map[position[0],position[1] + 2, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 3:
                if (position[0] != 1)
                {
                    if ((map[position[0] - 1,position[1], position[4]] == '*') && (map[position[0] - 2,position[1], position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
        }
        return flag;
    }

    int levelChangePossible(char[,,] map, int[] position)
    {
        int flag = 0;
        int otherLevel = 0;
        if (position[5] == 0){
            switch (position[4])
            {
                case 0:
                    otherLevel = 1;
                    break;
                case 1:
                    otherLevel = 0;
                    break;
            }
            switch (position[2])
            {
                case 0:
                    if (position[1] != 1)
                    {
                        if ((map[position[0], position[1] - 1, position[4]] == '*') && (map[position[0], position[1] - 1, otherLevel] == '*') && (map[position[0], position[1] - 2, otherLevel] == '*'))
                        {
                            flag = 1;
                        }
                    }
                    break;
                case 1:
                    if (position[0] != Constants.maxLength - 2)
                    {
                        if ((map[position[0] + 1, position[1], position[4]] == '*') && (map[position[0] + 1, position[1], otherLevel] == '*') && (map[position[0] + 2, position[1], otherLevel] == '*'))
                        {
                            flag = 1;
                        }
                    }
                    break;
                case 2:
                    if (position[1] != Constants.maxLength - 2)
                    {
                        if ((map[position[0], position[1] + 1, position[4]] == '*') && (map[position[0], position[1] + 1, otherLevel] == '*') && (map[position[0], position[1] + 2, otherLevel] == '*'))
                        {
                            flag = 1;
                        }
                    }
                    break;
                case 3:
                    if (position[0] != 1)
                    {
                        if ((map[position[0] - 1, position[1], position[4]] == '*') && (map[position[0] - 1, position[1], otherLevel] == '*') && (map[position[0] - 2, position[1], otherLevel] == '*'))
                        {
                            flag = 1;
                        }
                    }
                    break;
            }
        }
        return flag;
    }

    int rightPossible(char[,,] map, int[] position)
        {
        int flag = 0;
        switch (position[2])
        {
            case 0:
                if (position[0] != Constants.maxLength - 1)
                {
                    if ((map[position[0],position[1] - 1, position[4]] == '*') && (map[position[0] + 1,position[1] - 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 1:
                if (position[1] != Constants.maxLength - 1)
                {
                    if ((map[position[0] + 1,position[1], position[4]] == '*') && (map[position[0] + 1,position[1] + 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 2:
                if (position[0] != 0)
                {
                    if ((map[position[0],position[1] + 1, position[4]] == '*') && (map[position[0] - 1,position[1] + 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
            case 3:
                if (position[1] != 0)
                {
                    if ((map[position[0] - 1,position[1], position[4]] == '*') && (map[position[0] - 1,position[1] - 1, position[4]] == '*'))
                    {
                        flag = 1;
                    }
                }
                break;
        }
        return flag;
    }

    int leftLeft(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        char[,,] copyMap = new char[Constants.maxLength,Constants.maxLength,2];
	    int[] copyPosition = new int[6];
        int[,] copyQueue = new int[15,3];
	
	    int step = 1;

        copyPosition = positionCopier(position);
        copyMap = mapCopier(map);
        copyQueue = queueCopier(queue);

        left(copyMap, copyPosition, copyQueue, sentinel);
        step = step + 1;
	    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
	    return step;
    }

    int straightLeft(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        char[,,] copyMap = new char[Constants.maxLength,Constants.maxLength,2];
	    int[] copyPosition = new int[6];
        int[,] copyQueue = new int[15,3];
	
	    int step = 1;

        copyPosition = positionCopier(position);
        copyMap = mapCopier(map);
        copyQueue = queueCopier(queue);

        straight(copyMap, copyPosition, copyQueue, sentinel);
        step = step + 1;
	    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
	    return step;
    }

    int levelChangeLeft(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        char[,,] copyMap = new char[Constants.maxLength, Constants.maxLength, 2];
        int[] copyPosition = new int[6];
        int[,] copyQueue = new int[15, 3];

        int step = 1;

        copyPosition = positionCopier(position);
        copyMap = mapCopier(map);
        copyQueue = queueCopier(queue);

        levelChange(copyMap, copyPosition, copyQueue, sentinel);
        step = step + 1;
        step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
        return step;
    }

    int rightLeft(char[,,] map, int[] position, int[,] queue, int sentinel)
    {
        char[,,] copyMap = new char[Constants.maxLength,Constants.maxLength,2];
	    int[] copyPosition = new int[6];
        int[,] copyQueue = new int[15,3];
	
	    int step = 1;

        copyPosition = positionCopier(position);
        copyMap = mapCopier(map);
        copyQueue = queueCopier(queue);

        right(copyMap, copyPosition, copyQueue, sentinel);
        step = step + 1;
	    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
	    return step;
    }

    int leftHandMethod(char[,,] map, int[] position, int[,] queue, int sentinel, int step)
    {
        if (sentinel < 15)
        {
            char[,,] copyMap = new char[Constants.maxLength,Constants.maxLength,2];
		    int[] copyPosition = new int[6];
            int[,] copyQueue = new int[15,3];
		    int returnedStep;
            
            copyPosition = positionCopier(position);
            copyMap = mapCopier(map);
            copyQueue = queueCopier(queue);
            if (leftPossible(copyMap, copyPosition) == 1){
                left(copyMap, copyPosition, copyQueue, sentinel);
                step = step + 1;
			    if(step< 15){
				    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
			    }
			    else{
				    return(step);
			    }
			    if(returnedStep< 15){
				    step = step - 1;
                    copyPosition = positionCopier(position);
                    copyMap = mapCopier(map);
                    copyQueue = queueCopier(queue);
                    if (straightPossible(copyMap, copyPosition) == 1){
                        straight(copyMap, copyPosition, copyQueue, sentinel);
                        step = step + 1;
					    if(step< 15){
						    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
					    }
					    else{
						    return(step);
                        }
                        if (returnedStep < 15)
                        {
                            step = step - 1;
                            copyPosition = positionCopier(position);
                            copyMap = mapCopier(map);
                            copyQueue = queueCopier(queue);
                            if (levelChangePossible(copyMap, copyPosition) == 1)
                            {
                                levelChange(copyMap, copyPosition, copyQueue, sentinel);
                                step = step + 1;
                                if (step < 15)
                                {
                                    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                                }
                                else {
                                    return (step);
                                }
                                if (returnedStep < 15)
                                {
                                    step = step - 1;
                                    copyPosition = positionCopier(position);
                                    copyMap = mapCopier(map);
                                    copyQueue = queueCopier(queue);
                                    if (rightPossible(copyMap, copyPosition) == 1)
                                    {
                                        right(copyMap, copyPosition, copyQueue, sentinel);
                                        step = step + 1;
                                        if (step < 15)
                                        {
                                            returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                                            if (returnedStep == 15)
                                            {
                                                return (returnedStep);
                                            }
                                        }
                                        else {
                                            return (step);
                                        }
                                    }
                                }
                            }
                        }
					    else{
						    return(returnedStep);
					    }
				    }
			    }
			    else{
				    return(returnedStep);
			    }
		    }
		    else if(straightPossible(copyMap, copyPosition) == 1){
                straight(copyMap, copyPosition, copyQueue, sentinel);
                step = step + 1;
			    if(step< 15){
				    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
			    }
			    else{
				    return(step);
			    }
			    if(returnedStep< 15){
				    step = step - 1;
                    copyPosition = positionCopier(position);
                    copyMap = mapCopier(map);
                    copyQueue = queueCopier(queue);
				    if(rightPossible(copyMap, copyPosition) == 1){
                        right(copyMap, copyPosition, copyQueue, sentinel);
                        step = step + 1;
					    if(step< 15){
						    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
					    }
					    else{
						    return(step);
					    }
                        if (returnedStep< 15){
				            step = step - 1;
                            copyPosition = positionCopier(position);
                            copyMap = mapCopier(map);
                            copyQueue = queueCopier(queue);
				            if(levelChangePossible(copyMap, copyPosition) == 1){
                                levelChange(copyMap, copyPosition, copyQueue, sentinel);
                                step = step + 1;
					            if(step< 15){
						            step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
					            }
					            else{
						            return(step);
					            }
					            if(returnedStep < 15)
                                {
                                    step = step - 1;
                                    copyPosition = positionCopier(position);
                                    copyMap = mapCopier(map);
                                    copyQueue = queueCopier(queue);
                                    if (rightPossible(copyMap, copyPosition) == 1)
                                    {
                                        right(copyMap, copyPosition, copyQueue, sentinel);
                                        step = step + 1;
                                        if (step < 15)
                                        {
                                            step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                                        }
                                        else {
                                            return (step);
                                        }
                                        if (returnedStep < 15)
                                        {
                                            return (returnedStep);
                                        }
                                    }
                                }
				            }
			            }
			            else{
				            return(returnedStep);
			            }
				    }
			    }
			    else{
				    return(returnedStep);
			    }
            }
            else if (levelChangePossible(copyMap, copyPosition) == 1)
            {
                levelChange(copyMap, copyPosition, copyQueue, sentinel);
                step = step + 1;
                if (step < 15)
                {
                    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                }
                else {
                    return (step);
                }
                if (returnedStep < 15)
                {
                    step = step - 1;
                    copyPosition = positionCopier(position);
                    copyMap = mapCopier(map);
                    copyQueue = queueCopier(queue);
                    if (rightPossible(copyMap, copyPosition) == 1)
                    {
                        right(copyMap, copyPosition, copyQueue, sentinel);
                        step = step + 1;
                        if (step < 15)
                        {
                            step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                        }
                        else {
                            return (step);
                        }
                        if (returnedStep < 15)
                        {
                            step = step - 1;
                            copyPosition = positionCopier(position);
                            copyMap = mapCopier(map);
                            copyQueue = queueCopier(queue);
                            if (rightPossible(copyMap, copyPosition) == 1)
                            {
                                right(copyMap, copyPosition, copyQueue, sentinel);
                                step = step + 1;
                                if (step < 15)
                                {
                                    step = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
                                }
                                else {
                                    return (step);
                                }
                                if (returnedStep == 15)
                                {
                                    return (returnedStep);
                                }
                            }
                        }
                        else {
                            return (returnedStep);
                        }
                    }
                }
                else {
                    return (returnedStep);
                }
            }
            else if(rightPossible(copyMap, copyPosition) == 1){
                right(copyMap, copyPosition, copyQueue, sentinel);
                step = step + 1;
			    if(step< 15){
				    returnedStep = leftHandMethod(copyMap, copyPosition, copyQueue, sentinel, step);
			    }
			    else{
				    return(step);
			    }
			    if(returnedStep == 15){
				    return(returnedStep);
			    }
		    }
	    }
	    return step;
    }

    char[,,] mapCopier(char[,,] tempMap){
        char[,,] copyMap = new char[Constants.maxLength, Constants.maxLength, 2];
        
        int i;
        int j;
        int k;
        for (k = 0; k < 2; k++)
        {
            for (j = 0; j < Constants.maxLength; j++)
            {
                for (i = 0; i < Constants.maxLength; i++)
                {
                    copyMap[i, j, k] = tempMap[i, j, k];
                }
            }
        }
        return copyMap;
    }

    int[] positionCopier(int[] tempPosition)
    {
        int[] copyPosition = new int[6];

        int i;
        for (i = 0; i < 6; i++)
        {
            copyPosition[i] = tempPosition[i];
        }
        return copyPosition;
    }

    int[,] queueCopier(int[,] tempQueue)
    {
        int[,] copyQueue = new int[15, 3];
        int i;
        int j;

        for (j = 0; j < 3; j++)
        {
            for (i = 0; i < 15; i++)
            {
                copyQueue[i, j] = tempQueue[i, j];
            }
        }
        return copyQueue;
    }
}