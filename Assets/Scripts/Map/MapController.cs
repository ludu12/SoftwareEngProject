using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapController : MonoBehaviour, IMap {
    MapGenerationScript mapGenerator;

    public GameObject Ground;
    public GameObject GroundT;
    public GameObject BridgeU;
    public GameObject BridgeT;
    public GameObject BridgeF;
    public GameObject car;

    private int intialMapPieceCounter = 0;
    private const int maxIntialPieces = 10;
    private bool userGenerated = false;

    public float waitTime = 2f;

    // Use this for initialization
    void Start () {
        mapGenerator = new MapGenerationScript();
        mapGenerator.SetMapInterface(this);
        mapGenerator.Start();

        NotificationCenter.DefaultCenter().AddObserver(this, "UserGenerateMapPiece");
        NotificationCenter.DefaultCenter().AddObserver(this, "IncreaseLevel");

        StartCoroutine(GenerateMapPiece());
        Instantiate(car, Vector3.zero, Quaternion.identity);
    }

    void IncreaseLevel()
    {
        waitTime = Mathf.Clamp(waitTime - 0.1f, 0.5f, 2f);
        Debug.Log("NEW LEVEL!");
    }

    IEnumerator GenerateMapPiece()
    {
        yield return new WaitForSeconds(8f);
        while (true)
        {
            if (userGenerated)
            {
                userGenerated = false;
                yield return new WaitForSeconds(waitTime);
            }

            mapGenerator.GenerateMapPiece();
            yield return new WaitForSeconds(waitTime);
        }
    }

    void UserGenerateMapPiece(Notification data)
    {
        GameObject piece = (GameObject)data.data;
        if (mapGenerator.isFrontPiece(piece))
        {
            Debug.Log("User Generated a new piece");
            userGenerated = true;
            mapGenerator.GenerateMapPiece();
        }
    }

    void OnDestroy()
    {
        NotificationCenter.DefaultCenter().RemoveObserver(this, "UserGenerateMapPiece");
        NotificationCenter.DefaultCenter().RemoveObserver(this, "IncreaseLevel");
    }

    #region Interface implementation

    public void DestroyThis(GameObject go)
    {
        Destroy(go);
    }

    public GameObject InstantiateGround(Vector3 startingPos, Quaternion startingRot)
    {
        GameObject piece = (GameObject)Instantiate(Ground, startingPos, startingRot);
        //piece.GetComponent<MapPrefab>().isInitialPiece = (intialMapPieceCounter < maxIntialPieces) ? true : false;
        //intialMapPieceCounter++;
        return piece;
    }

    public GameObject InstantiateGroundT(Vector3 startingPos, Quaternion startingRot)
    {
        GameObject piece = (GameObject)Instantiate(GroundT, startingPos, startingRot);
        //piece.GetComponent<MapPrefab>().isInitialPiece = (intialMapPieceCounter < maxIntialPieces) ? true : false;
        //intialMapPieceCounter++;
        return piece;
    }

    public GameObject InstantiateBridgeU(Vector3 startingPos, Quaternion startingRot)
    {
        GameObject piece = (GameObject)Instantiate(BridgeU, startingPos, startingRot);
        //piece.GetComponent<MapPrefab>().isInitialPiece = (intialMapPieceCounter < maxIntialPieces) ? true : false;
        //intialMapPieceCounter++;
        return piece;
    }

    public GameObject InstantiateBridgeT(Vector3 startingPos, Quaternion startingRot)
    {
        GameObject piece = (GameObject)Instantiate(BridgeT, startingPos, startingRot);
        //piece.GetComponent<MapPrefab>().isInitialPiece = (intialMapPieceCounter < maxIntialPieces) ? true : false;
        //intialMapPieceCounter++;
        return piece;
    }

    public GameObject InstantiateBridgeF(Vector3 startingPos, Quaternion startingRot)
    {
        GameObject piece = (GameObject)Instantiate(BridgeF, startingPos, startingRot);
        //piece.GetComponent<MapPrefab>().isInitialPiece = (intialMapPieceCounter < maxIntialPieces) ? true : false;
        //intialMapPieceCounter++;
        return piece;
    }

    #endregion
}
