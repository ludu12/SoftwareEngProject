using UnityEngine;
using System.Collections;

public class MapPrefab : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col + " has entered new box");
        GetComponent<BoxCollider>().enabled = false; // disable the box collider so that we cannot re enter this one
        NotificationCenter.DefaultCenter().PostNotification(this, "GenerateMapPiece");
    }
}
