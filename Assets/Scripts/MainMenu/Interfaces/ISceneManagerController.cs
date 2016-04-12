using UnityEngine;
using System.Collections;

public interface ISceneManagerController {

    void LoadScene(string scene);

    string GetCurrentScene();

	void Quit();
}
