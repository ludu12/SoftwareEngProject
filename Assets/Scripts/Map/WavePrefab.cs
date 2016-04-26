using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WavePrefab : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.transform.root.gameObject + " hit wave");
        Destroy(col.transform.root.gameObject);
        NotificationCenter.DefaultCenter().PostNotification(this, "OnPlayerDeath");
    }
}
