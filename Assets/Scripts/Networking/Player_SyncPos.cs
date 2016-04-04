using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_SyncPos : NetworkBehaviour
{

    [SyncVar]
    private Vector3 syncPos; // automatically sends this to all clients

    [SerializeField]Transform myTransform;
    [SerializeField]float lerpRate = 15;

    private Vector3 lastPos;
    private float posThreshold = 0.5f;


    private NetworkClient nClient;
    private int latency;
    private Text latencyText;

    void Start()
    {
        nClient = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().client;
        latencyText = GameObject.Find("Text").GetComponent<Text>();
    }

    void Update()
    {
        ShowLatency();
        LerpPos();
    }

    void FixedUpdate()
    {
        TransmitPos();
    }

    void LerpPos()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
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

    void ShowLatency()
    {
        if(isLocalPlayer)
        {
            latency = nClient.GetRTT();
            latencyText.text = latency.ToString();
        }
    }
}