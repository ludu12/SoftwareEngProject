using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncPosRot : NetworkBehaviour {
    
    [SyncVar]
    private Vector3 syncPos; // automatically sends this to all clients
    [SyncVar]
    private Quaternion syncRot; // automatically sends this to all clients

    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;

    private Vector3 lastPos;
    private Quaternion lastRot;
    private float posThreshold = 0.5f;
    private float rotThreshold = 5f;


    void FixedUpdate()
    {
        TransmitPosRot();
        LerpPosRot();
    }

    void LerpPosRot()
    {
        if(!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncRot, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePosRotToServer(Vector3 pos, Quaternion rot)
    {
        // this command will be recieved by a client from a server
        syncPos = pos;
        syncRot = rot;
        Debug.Log("Command for movement");
    }

    [ClientCallback]
    void TransmitPosRot()
    {
        // sends my position to the servers
        if (isLocalPlayer)
        {
            // only send if the player has moved or rotated
            if ((Vector3.Distance(myTransform.position, lastPos) > posThreshold) || (Quaternion.Angle(myTransform.rotation, lastRot) > rotThreshold))
            {
                CmdProvidePosRotToServer(myTransform.position, myTransform.rotation);
                lastPos = myTransform.position;
                lastRot = myTransform.rotation;
            }
        }
    }
}