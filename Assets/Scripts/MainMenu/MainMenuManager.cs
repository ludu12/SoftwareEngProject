﻿using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnStartButtonClick()
    {
        Application.LoadLevel("Game");
    }

	public void OnHowToPlayButtonClick()
	{
		Application.LoadLevel ("Controls");
	}
}
