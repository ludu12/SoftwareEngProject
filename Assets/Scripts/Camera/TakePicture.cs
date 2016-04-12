using UnityEngine;
using System.Collections;

public class TakePicture : MonoBehaviour {

    public int resWidth = 2550;
    public int resHeight = 3300;

    Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        NotificationCenter.DefaultCenter().AddObserver(this, "OnMapChange");
    }

    public static string ScreenShotName()
    {
        return string.Format("{0}/Screenshots/map.png",Application.dataPath);
    }

    void OnMapChange()
    {
        camera.enabled = true;
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName();
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
        camera.enabled = false;
    }
}
