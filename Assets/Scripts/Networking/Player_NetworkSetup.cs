using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_NetworkSetup : NetworkBehaviour, IPlayerSetup {

    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    public GameObject body;
    public GameObject rightMirror;
    public GameObject leftMirror;

    // Textures for body
    public Material magentaBody;
    public Material redBody;
    public Material cyanBody;
    public Material blueBody;
    public Material greenBody;
    public Material yellowBody;

    // Textures for mirror
    public Material magentaMirror;
    public Material redMirror;
    public Material cyanMirror;
    public Material blueMirror;
    public Material greenMirror;
    public Material yellowMirror;

    [SerializeField]
	Camera carCam;

	[SerializeField]
	AudioListener audioListener;

    public PlayerSetup playerSetup;

    private NetworkClient nClient;
    private int latency;
    private Text latencyText;

    private void OnEnable()
    {
        playerSetup = new PlayerSetup();
        playerSetup.SetNetworkSetup(this);
        // This is messy, but helps with testing..
        playerSetup.magentaBody = magentaBody;
        playerSetup.redBody = redBody;
        playerSetup.cyanBody = cyanBody;
        playerSetup.blueBody = blueBody;
        playerSetup.greenBody = greenBody;
        playerSetup.yellowBody = yellowBody;
        playerSetup.magentaMirror = magentaMirror;
        playerSetup.redMirror = redMirror;
        playerSetup.cyanMirror = cyanMirror;
        playerSetup.blueMirror = blueMirror;
        playerSetup.greenMirror = greenMirror;
        playerSetup.yellowMirror = yellowMirror;
    }

    // Use this for initialization
    void Start () {
		if (isLocalPlayer) {
			GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().enabled = true;
            GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = true;

            carCam.enabled = true;
			carCam.GetComponent<SwitchCameraController>().enabled = true;
			audioListener.enabled = true;
		}
        GameObject[] goArray = { body, leftMirror, rightMirror };

        // Test this
        playerSetup.SetUpColor(color, goArray);

        nClient = GameObject.Find("LobbyManager").GetComponent<NetworkLobbyManager>().client;
        latencyText = GameObject.Find("LatencyText").GetComponent<Text>();
    }

    void Update()
    {
        ShowLatency();
    }

    void ShowLatency()
    {
        if (isLocalPlayer)
        {
            latency = nClient.GetRTT();
            latencyText.text = latency.ToString() + " ms";
        }
    }

    #region PlayerSetup implementation

    public void SetMaterialForGameObject(GameObject go, Material mat)
    {
        go.GetComponent<Renderer>().material = mat;
    }

    #endregion
}
