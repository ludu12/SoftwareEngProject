using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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
			GetComponent<CarMotor>().enabled = true;
			carCam.enabled = true;
			carCam.GetComponent<SwitchCamera>().enabled = true;
			audioListener.enabled = true;
		}
        GameObject[] goArray = { body, leftMirror, rightMirror };
        playerSetupContoller.SetUpColor(color, goArray);
	}

    #region PlayerSetup implementation

    public void SetMaterialForGameObject(GameObject go, Material mat)
    {
        go.GetComponent<Renderer>().material = mat;
    }

    #endregion
}
