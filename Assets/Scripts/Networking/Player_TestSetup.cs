using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_TestSetup : NetworkBehaviour {

    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SerializeField]
	Camera carCam;

	[SerializeField]
	AudioListener audioListener;

    private NetworkClient nClient;
    private int latency;
    private Text latencyText;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;

            carCam.enabled = true;
            audioListener.enabled = true;
        }
    }
}
