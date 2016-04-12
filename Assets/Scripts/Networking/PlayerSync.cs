using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSync {

    IPlayerSync playerSyncMono;

    private Vector3 lastPos;
    private Quaternion lastRot;
    private float posThreshold = 0.5f;
    private float rotThreshold = 5f;

    //[SerializeField]
    float lerpRate = 15;
    private float normalLerpRate = 15;
    private float fasterLerpRate = 50;

    public List<Vector3> syncPosList = new List<Vector3>();
    public List<Quaternion> syncRotList = new List<Quaternion>();
    private float closeEnough = 0.3f;

    /// <summary>
    /// Compares the inputed position data against our own and returns true if it has changed passed our thresholds
    /// </summary>
    /// <param name="currentPosition"></param>
    /// <param name="currentRotation"></param>
    /// <returns></returns>
    public bool HasMoved(Vector3 currentPosition, Quaternion currentRotation)
    {
        // Check if we have moved, if yes then return true
        if (Vector3.Distance(currentPosition, lastPos) > posThreshold)
            return true;
        // then check if we rotated
        if ((Quaternion.Angle(currentRotation, lastRot) > rotThreshold))
            return true;

        return false;
    }

    /// <summary>
    /// Simply sets our private variables
    /// </summary>
    /// <param name="newPosition"></param>
    /// <param name="newRotation"></param>
    public void SetLastPositionRotation(Vector3 newPosition, Quaternion newRotation)
    {
        lastPos = newPosition;
        lastRot = newRotation;
    }

    /// <summary>
    /// Wrapper for interface
    /// </summary>
    public Vector3 CallLerpPosition(Vector3 newPos)
    {
        return playerSyncMono.LerpPosition(newPos, lerpRate);
    }

    /// <summary>
    /// Wrapper for interface
    /// </summary>
    public Quaternion CallLerpRotation(Quaternion newRot)
    {
        return playerSyncMono.LerpRotation(newRot, lerpRate);
    }


    /// <summary>
    /// Uses previous values to interpolate
    /// </summary>
    /// <param name="currentPosition"></param>
    /// <returns></returns>
    public Vector3 CallHistoricalLerpPosition(Vector3 currentPosition)
    {
        // make sure list isnt empty
        if (syncPosList.Count > 1)
        {
            // now check if this position is close enough or not
            Vector3 position = Vector3.Lerp(syncPosList[0], syncPosList[1], Time.deltaTime * lerpRate);
            // if we are close enough, then remove this position
            if (Vector3.Distance(position, syncPosList[1]) < posThreshold)
                syncPosList.RemoveAt(0);

            if (syncPosList.Count > 10)
                lerpRate = fasterLerpRate;
            else
                lerpRate = normalLerpRate;

            Debug.Log(syncPosList.Count.ToString());
            return position;
        }
        else
        {
            return currentPosition; // return the same vector if we have no list
        }

    }

    /// <summary>
    /// Uses previous values to interpolate
    /// </summary>
    /// <param name="currentRotation"></param>
    /// <returns></returns>
    public Quaternion CallHistoricalLerpRotation(Quaternion currentRotation)
    {
        // make sure list isnt empty
        if (syncRotList.Count > 0)
        {
            // now check if this position is close enough or not
            Quaternion rotation = playerSyncMono.LerpRotation(syncRotList[0], lerpRate);
            // if we are close enough, then remove this position
            if (Quaternion.Angle(rotation, syncRotList[0]) < rotThreshold)
                syncRotList.RemoveAt(0);

            if (syncRotList.Count > 10)
                lerpRate = fasterLerpRate;
            else
                lerpRate = normalLerpRate;

            Debug.Log(syncRotList.Count.ToString());
            return rotation;
        }
        else
        {
            return currentRotation; // return the same rotation if we have no list
        }

    }

    /// <summary>
    /// Sets the player sync interface
    /// </summary>
    /// <param name="playerSync"></param>
    public void SetPlayerSync(IPlayerSync playerSync)
    {
        this.playerSyncMono = playerSync;
    }

}
