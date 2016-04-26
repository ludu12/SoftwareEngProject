using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkingMapPrefab : NetworkBehaviour {

    public Vector2 previousPosition;

    [SyncVar]
    public bool hasBeenVisited = false;

    [SyncVar]
    public Quaternion rotation;

    void Update()
    {
        this.transform.rotation = rotation;
    }

    void OnTriggerEnter(Collider col)
    {
        // if we have visted the map, then dont post a message to generate a map piece again
        if (!hasBeenVisited)
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "UserGenerateMapPiece", this.gameObject);
            hasBeenVisited = true;
        }
    }
}
