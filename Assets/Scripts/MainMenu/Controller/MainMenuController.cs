using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    private ISceneManager sceneManager;

    // Load the scene
    public void LoadScene(string scene)
    {
        sceneManager.LoadScene(scene);
    }

    // Set SceneManager controller
    public void SetSceneManangerController(ISceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
    }

}
