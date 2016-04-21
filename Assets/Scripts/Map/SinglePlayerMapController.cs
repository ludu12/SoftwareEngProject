using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SinglePlayerMapController : MonoBehaviour, IDestroyInstantiate, INotificationCenter {

    public bool _debugging = false;

    MapGenerationScript mapGenerator;


    public GameObject Ground;
    public GameObject GroundT;
    public GameObject BridgeU;
    public GameObject BridgeT;
    public GameObject BridgeF;
    public GameObject car;

    private bool userGenerated = false;

    public float waitTime = 2f;

    // Use this for initialization
    void Start () {
        mapGenerator = new MapGenerationScript();
        mapGenerator.SetMapInterface(this);
        mapGenerator.SetNotificationInterface(this);
        mapGenerator.Ground = Ground;
        mapGenerator.GroundT = GroundT;
        mapGenerator.BridgeF = BridgeF;
        mapGenerator.BridgeT = BridgeT;
        mapGenerator.BridgeU = BridgeU;
        mapGenerator.Start();

        NotificationCenter.DefaultCenter().AddObserver(this, "UserGenerateMapPiece");
        NotificationCenter.DefaultCenter().AddObserver(this, "IncreaseLevel");

        if(!_debugging)
            StartCoroutine(GenerateMapPiece());
        Instantiate(car, new Vector3(0,0.25f,0), Quaternion.identity);
    }

    void IncreaseLevel()
    {
        waitTime = Mathf.Clamp(waitTime - 0.1f, 0.5f, 2f);
        Debug.Log("NEW LEVEL!");
    }

    IEnumerator GenerateMapPiece()
    {
        yield return new WaitForSeconds(10f);
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

    #region Destroy and Instatiate interface implementation

    public void DestroyThis(GameObject go)
    {
        Destroy(go);
    }

    private Vector2 _previousPosition = new Vector3(0,0);
    public GameObject InstantiateGameObject(GameObject go, Vector3 startingPos, Quaternion startingRot)
    {
        GameObject piece = (GameObject)Instantiate(go, startingPos, startingRot);
        piece.GetComponent<MapPrefab>().previousPosition = _previousPosition;
        _previousPosition.Set(startingPos.x, startingPos.z);
        return piece;
    }
    #endregion

    #region Notificaiton interface implementation
    public void AddObserver(string name, object sender = null)
    {
        throw new NotImplementedException();
    }

    public void PostNotification(string name, object data = null)
    {
        if (data != null)
            NotificationCenter.DefaultCenter().PostNotification(this, name, data);
        else
            NotificationCenter.DefaultCenter().PostNotification(this, name);
    }

    #endregion
}
