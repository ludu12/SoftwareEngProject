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

    public PlayerSetupContoller playerSetupContoller;

    private NetworkClient nClient;
    private int latency;
    private Text latencyText;

    private void OnEnable()
    {
        playerSetupContoller = new PlayerSetupContoller();
        playerSetupContoller.SetNetworkSetup(this);
        // This is messy, but helps with testing..
        playerSetupContoller.magentaBody = magentaBody;
        playerSetupContoller.redBody = redBody;
        playerSetupContoller.cyanBody = cyanBody;
        playerSetupContoller.blueBody = blueBody;
        playerSetupContoller.greenBody = greenBody;
        playerSetupContoller.yellowBody = yellowBody;
        playerSetupContoller.magentaMirror = magentaMirror;
        playerSetupContoller.redMirror = redMirror;
        playerSetupContoller.cyanMirror = cyanMirror;
        playerSetupContoller.blueMirror = blueMirror;
        playerSetupContoller.greenMirror = greenMirror;
        playerSetupContoller.yellowMirror = yellowMirror;
    }

    // Use this for initialization
    void Start () {
		if (isLocalPlayer) {
			GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().enabled = true;
            GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = true;

            carCam.enabled = true;
			carCam.GetComponent<SwitchCamera>().enabled = true;
			audioListener.enabled = true;
		}
        GameObject[] goArray = { body, leftMirror, rightMirror };

        // Test this
        playerSetupContoller.SetUpColor(color, goArray);

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
