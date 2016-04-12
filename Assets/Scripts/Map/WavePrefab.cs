using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WavePrefab : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col + " hit wave");
        Destroy(col.transform.parent.gameObject);
        GameObject.Find("LoseMessage").GetComponent<Text>().enabled = true;
    }
}
