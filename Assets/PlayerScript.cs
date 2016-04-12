using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    private int score = 0;
    Text scoreText;
    Text timerText;
    string minutes;
    string seconds;
    float timer = 0;

	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerateMapPiece");
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        timerText = GameObject.Find("timerText").GetComponent<Text>();
    }

    void GenerateMapPiece()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        timer += Time.deltaTime;

        minutes = Mathf.Floor(timer / 60).ToString("00");
        seconds = Mathf.Floor(timer % 60).ToString("00");

        timerText.text = minutes + ":" + seconds;
    }

}
