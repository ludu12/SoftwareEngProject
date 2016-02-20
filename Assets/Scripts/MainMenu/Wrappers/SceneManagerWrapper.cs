using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : MonoBehaviour, ISceneManager{

    // Wrapper for scene manager load scene method
    void ISceneManager.LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
