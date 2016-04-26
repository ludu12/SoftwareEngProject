using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

[NetworkSettings(channel = 0, sendInterval = 0.033f)]
public class PlayerSyncController : NetworkBehaviour, IPlayerSync {

    [SyncVar(hook = "SyncPositionValues")]
    private Vector3 syncPos = new Vector3(); // automatically sends this to all clients
    [SyncVar(hook = "SyncRotationValues")]
    private Quaternion syncRot = new Quaternion(); // automatically sends this to all clients

    [SerializeField]
    Rigidbody myRigidbody;

    [SerializeField]
    private bool useHistoricalLerping = false;

    PlayerSync playerSync;

    #region Monobehaviour functions
    void OnEnable()
    {
        playerSync = new PlayerSync();
        playerSync.SetPlayerSync(this);
    }

    void FixedUpdate()
    {
        TransmitPosRot(); // transmit our position to server
    }

    void Update()
    {
        if (!isLocalPlayer && useHistoricalLerping) // if we are not local player then we may have to move so call interface functions
        {
            myRigidbody.position = playerSync.CallHistoricalLerpPosition(myRigidbody.position);
            myRigidbody.rotation = playerSync.CallHistoricalLerpRotation(myRigidbody.rotation);
        }
        else if (!isLocalPlayer)
        {
            myRigidbody.position = playerSync.CallLerpPosition(syncPos);
            myRigidbody.rotation = playerSync.CallLerpRotation(syncRot);
        }
    }

    #endregion

    #region Networking

    [Command]
    void CmdProvidePosRotToServer(Vector3 pos, Quaternion rot)
    {
        // this command will be recieved by a client from a server
        syncPos = pos;
        syncRot = rot;
    }

    [ClientCallback]
    void TransmitPosRot()
    {
        // sends my position to the servers
        if (isLocalPlayer)
        {
            // only send if the player has moved or rotated
            if (playerSync.HasMoved(myRigidbody.position, myRigidbody.rotation))
            {
                CmdProvidePosRotToServer(myRigidbody.position, myRigidbody.rotation);
                playerSync.SetLastPositionRotation(myRigidbody.position, myRigidbody.rotation);
            }
        }
    }

    [Client]
    void SyncPositionValues(Vector3 lastestPos)
    {
        syncPos = lastestPos;
        playerSync.syncPosList.Add(syncPos);
    }

    [Client]
    void SyncRotationValues(Quaternion lastestRot)
    {
        syncRot = lastestRot;
        playerSync.syncRotList.Add(syncRot);
    }

    #endregion

    #region Interface Implementation

    public Vector3 LerpPosition(Vector3 newPos, float lerpRate)
    {
        return Vector3.Lerp(myRigidbody.position, newPos, Time.deltaTime * lerpRate);
    }

    public Quaternion LerpRotation(Quaternion newRot, float lerpRate)
    {
        return Quaternion.Lerp(myRigidbody.rotation, newRot, Time.deltaTime * lerpRate);
    }

    #endregion
}