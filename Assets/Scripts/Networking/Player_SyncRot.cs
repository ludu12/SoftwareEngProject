using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_SyncRot : NetworkBehaviour
{
    [SyncVar]
    private Quaternion syncRot; // automatically sends this to all clients

    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 15;

    private Quaternion lastRot;
    private float rotThreshold = 5f;

    void Update()
    {
        LerpRot();
    }

    void FixedUpdate()
    {
        TransmitRot();
    }

    void LerpRot()
    {
        if (!isLocalPlayer)
        {
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncRot, Time.deltaTime * lerpRate);
            Debug.Log(Time.deltaTime.ToString());
        }
    }

    [Command]
    void CmdProvideRotToServer(Quaternion rot)
    {
        // this command will be recieved by a client from a server
        syncRot = rot;
        //Debug.Log("Command for movement");
    }

    [Client]
    void TransmitRot()
    {
        // sends my position to the servers
        if (isLocalPlayer && Quaternion.Angle(myTransform.rotation, lastRot) > rotThreshold)
        {
            CmdProvideRotToServer(myTransform.rotation);
            lastRot = myTransform.rotation;
        }
    }
}