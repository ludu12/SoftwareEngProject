using UnityEngine;
using System.Collections;

public class MapPrefab : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        GetComponent<BoxCollider>().enabled = false; // disable the box collider so that we cannot re enter this one

        NotificationCenter.DefaultCenter().PostNotification(this, "UserGenerateMapPiece", this.gameObject);
        NotificationCenter.DefaultCenter().PostNotification(this, "ScoreIncrease");
    }
    
    void OnDestroy()
    {
        NotificationCenter.DefaultCenter().RemoveObserver(this, "UserGenerateMapPiece");
        NotificationCenter.DefaultCenter().RemoveObserver(this, "ScoreIncrease");
    }
}
