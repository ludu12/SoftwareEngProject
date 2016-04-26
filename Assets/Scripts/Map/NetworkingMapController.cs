using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class NetworkingMapController : NetworkBehaviour, IDestroyInstantiate, INotificationCenter {

    public bool _debugging = false;

    MapGenerationScript mapGenerator;
    
    public GameObject Ground;
    public GameObject GroundT;
    public GameObject BridgeU;
    public GameObject BridgeT;
    public GameObject BridgeF;
    public GameObject car;

    public GameObject startPiece;

    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "UserGenerateMapPiece");
        if (isServer)
        {
            mapGenerator = new MapGenerationScript();
            mapGenerator.SetMapInterface(this);
            mapGenerator.SetNotificationInterface(this);
            mapGenerator.Ground = Ground;
            mapGenerator.GroundT = GroundT;
            mapGenerator.BridgeF = BridgeF;
            mapGenerator.BridgeT = BridgeT;
            mapGenerator.BridgeU = BridgeU;
            mapGenerator.Start(startPiece);
        }
    }

    public void UserGenerateMapPiece(Notification data)
    {
        CmdUserGenerateMapPiece((GameObject)data.data);
    }

    [Command]
    void CmdUserGenerateMapPiece(GameObject piece)
    {
        if (mapGenerator.isFrontPiece(piece))
        {
            mapGenerator.GenerateMapPiece();
        }
    }

    void OnDestroy()
    {
        NotificationCenter.DefaultCenter().RemoveObserver(this, "UserGenerateMapPiece");
    }

    #region Destroy and Instatiate interface implementation

    public void DestroyThis(GameObject go)
    {
        NetworkServer.Destroy(go);
    }

    private Vector2 _previousPosition = new Vector3(0, 0);
    int i = 0;
    public GameObject InstantiateGameObject(GameObject go, Vector3 startingPos, Quaternion startingRot)
    {
        Debug.Log(startingRot);
        GameObject piece = (GameObject)Instantiate(go, startingPos, startingRot);
        piece.name = ((i++) % 15).ToString();
        piece.GetComponent<NetworkingMapPrefab>().rotation = startingRot;
        piece.GetComponent<NetworkingMapPrefab>().previousPosition = _previousPosition;
        _previousPosition.Set(startingPos.x, startingPos.z);
        NetworkServer.Spawn(piece);
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
