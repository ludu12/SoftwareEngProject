using UnityEngine;
using System.Collections;

public class PlayerSetupContoller {

    public IPlayerSetup networkSetup;

    // Materials for body
    public Material magentaBody;
    public Material redBody;
    public Material cyanBody;
    public Material blueBody;
    public Material greenBody;
    public Material yellowBody;

    // Materials for mirror
    public Material magentaMirror;
    public Material redMirror;
    public Material cyanMirror;
    public Material blueMirror;
    public Material greenMirror;
    public Material yellowMirror;

    /// <summary>
    /// I know this is long but yeah..
    /// Determines what color to use and sets all game objects in array to their respective materials
    /// Make sure gameobject array has 3 objects!
    /// </summary>
    public void SetUpColor(Color color, GameObject[] goArray)
    {
        if (color == Color.magenta)
        {
            networkSetup.SetMaterialForGameObject(goArray[0], magentaBody);
            networkSetup.SetMaterialForGameObject(goArray[1], magentaMirror);
            networkSetup.SetMaterialForGameObject(goArray[2], magentaMirror);
        }
        else if (color == Color.red)
        {
            networkSetup.SetMaterialForGameObject(goArray[0], redBody);
            networkSetup.SetMaterialForGameObject(goArray[1], redMirror);
            networkSetup.SetMaterialForGameObject(goArray[2], redMirror);
        }
        else if (color == Color.cyan)
        {
            networkSetup.SetMaterialForGameObject(goArray[0], cyanBody);
            networkSetup.SetMaterialForGameObject(goArray[1], cyanMirror);
            networkSetup.SetMaterialForGameObject(goArray[2], cyanMirror);
        }
        else if (color == Color.blue)
        {
            networkSetup.SetMaterialForGameObject(goArray[0], blueBody);
            networkSetup.SetMaterialForGameObject(goArray[1], blueMirror);
            networkSetup.SetMaterialForGameObject(goArray[2], blueMirror);
        }
        else if (color == Color.green)
        {
            networkSetup.SetMaterialForGameObject(goArray[0], greenBody);
            networkSetup.SetMaterialForGameObject(goArray[1], greenMirror);
            networkSetup.SetMaterialForGameObject(goArray[2], greenMirror);
        }
        else if (color == Color.yellow)
        {
            networkSetup.SetMaterialForGameObject(goArray[0], yellowBody);
            networkSetup.SetMaterialForGameObject(goArray[1], yellowMirror);
            networkSetup.SetMaterialForGameObject(goArray[2], yellowMirror);
        }
    }

    public void SetNetworkSetup(IPlayerSetup networkSetup)
    {
        this.networkSetup = networkSetup;
    }
}
