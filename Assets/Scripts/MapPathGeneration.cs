using UnityEngine;
using System.Collections;
using CyberCommon;

public class MapPathGeneration : MonoBehaviour {

    private LimitedQueue<int> mapPath;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void StartupLogic()
    {
        mapPath.limit = 15;
        mapPath.Enqueue(0);
        for(int i = 0; i < 14; i++)
        {
            int r = (rand() % 3) - 1;
        }
    }

    void sumMapPath(int i)
    {

    }
}
