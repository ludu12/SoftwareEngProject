using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapPrefab : MonoBehaviour
{

    public Vector2 previousPosition;

    private bool hasBeenVisited = false;

    void OnTriggerEnter(Collider col)
    {
            //GetComponent<BoxCollider>().enabled = false; // disable the box collider so that we cannot re enter this one

            // if we have visted the map, then dont post a message to generate a map piece again
            if (!hasBeenVisited)
            {
                NotificationCenter.DefaultCenter().PostNotification(this, "UserGenerateMapPiece", this.gameObject);
                NotificationCenter.DefaultCenter().PostNotification(this, "OnCarScore");
                hasBeenVisited = true;
            }

            Vector2 myPosition = new Vector2();
            myPosition.Set(this.transform.position.x, this.transform.position.z);

            // always post that the car has entered though
            NotificationCenter.DefaultCenter().PostNotification(this, "OnCarEnter", myPosition - previousPosition);
    }
}
