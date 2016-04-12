using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

[NetworkSettings(channel = 0, sendInterval = 0.1f)]
public class Player_SyncPos : NetworkBehaviour
{

    [SyncVar(hook = "SyncPositionValues")]
    private Vector3 syncPos; // automatically sends this to all clients

    [SerializeField]
    Transform myTransform;

    private float lerpRate;
    [SerializeField]
    private float normalLerpRate = 15;
    [SerializeField]
    private float fasterLerpRate = 25;

    private Vector3 lastPos;
    private float posThreshold = 0.5f;


    private NetworkClient nClient;
    private int latency;
    private Text latencyText;

    private List<Vector3> syncPosList = new List<Vector3>();
    [SerializeField]
    private bool useHistoricalLerping = false;
    private float closeEnough = 0.3f;

    void Start()
    {
        nClient = GameObject.Find("LobbyManager").GetComponent<NetworkLobbyManager>().client;
        latencyText = GameObject.Find("LatencyText").GetComponent<Text>();
        lerpRate = normalLerpRate;
    }

    void Update()
    {
        LerpPos();
        ShowLatency();
    }

    void FixedUpdate()
    {
        TransmitPos();
    }

    void LerpPos()
    {
        if (!isLocalPlayer)
        {
            if(useHistoricalLerping)
            {
                HistoricalLerping();
            }
            else
            {
                OrdinaryLerping();
            }
        }
    }

    [Command]
    void CmdProvidePosToServer(Vector3 pos)
    {
        // this command will be recieved by a client from a server
        syncPos = pos;
        //Debug.Log("Command for movement");
    }

    [ClientCallback]
    void TransmitPos()
    {
        // sends my position to the servers
        if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > posThreshold)
        {
            CmdProvidePosToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }

    [Client]
    void SyncPositionValues(Vector3 lastestPos)
    {
        syncPos = lastestPos;
        syncPosList.Add(syncPos);
    }

    void ShowLatency()
    {
        if(isLocalPlayer)
        {
            latency = nClient.GetRTT();
            latencyText.text = latency.ToString();
        }
    }

    void OrdinaryLerping()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
    }

    void HistoricalLerping()
    {
        // make sure list isnt empty
        if(syncPosList.Count > 0)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPosList[0], Time.deltaTime * lerpRate);

            // if we are close enough, then remove this position
            if(Vector3.Distance(myTransform.position, syncPosList[0]) < closeEnough)
            {
                syncPosList.RemoveAt(0);
            }

            if(syncPosList.Count > 10)
            {
                lerpRate = fasterLerpRate;
            }
            else
            {
                lerpRate = normalLerpRate;
            }

            Debug.Log(syncPosList.Count.ToString());
        }
    }

}