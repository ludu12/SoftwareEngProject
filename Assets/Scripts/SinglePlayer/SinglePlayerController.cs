using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SinglePlayerController : MonoBehaviour, INotificationCenter {

    Text scoreText;
    Text timerText;
    Text messageText;

    SinglePlayer singlePlayer;
    Vector2 mapDirection = new Vector3(0,0);

    private bool hasStarted = false;

    // Use this for initialization
    IEnumerator Start () {

        if (GameObject.Find("Message") != null)
            messageText = GameObject.Find("Message").GetComponent<Text>();

        messageText.text = "3";
        yield return new WaitForSeconds(1);
        messageText.text = "2";
        yield return new WaitForSeconds(1);
        messageText.text = "1";
        yield return new WaitForSeconds(1);
        messageText.text = "GO!";
        GetComponent<CarController>().canMove = true;

        NotificationCenter.DefaultCenter().AddObserver(this, "OnCarEnter");
        NotificationCenter.DefaultCenter().AddObserver(this, "OnCarScore");

        if (GameObject.Find("ScoreText") != null)
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        if (GameObject.Find("TimerText") != null)
            timerText = GameObject.Find("TimerText").GetComponent<Text>();
        if (GameObject.Find("Message") != null)
            messageText = GameObject.Find("Message").GetComponent<Text>();

        singlePlayer = new SinglePlayer();
        singlePlayer.SetNotificationInterface(this);

        yield return new WaitForSeconds(1);
        messageText.text = "";
        StartCoroutine(Run());
    }

    void OnCarScore()
    {
        string score = "Score: " + singlePlayer.ScoreIncrease();
        if (scoreText != null)
            scoreText.text = score;
    }

    void OnCarEnter(Notification data)
    {
        mapDirection = (Vector2)data.data;
    }

    Vector2 myPostion = new Vector2();
    IEnumerator Run()
    {
        while (true)
        {
            myPostion.Set(transform.forward.x, transform.forward.z);
            if (!singlePlayer.IsFacingCorrectWay(mapDirection, myPostion))
                messageText.text = "WRONG WAY!";
            else
                messageText.text = "";

            string timer = singlePlayer.OnTimer(Time.deltaTime);
            if (timerText != null)
                timerText.text = timer;

            yield return null;
        }
    }

    void OnDestroy()
    {
        NotificationCenter.DefaultCenter().RemoveObserver(this, "OnPlayerDeath");
        NotificationCenter.DefaultCenter().RemoveObserver(this, "OnCarScore");
        NotificationCenter.DefaultCenter().RemoveObserver(this, "OnCarEnter");
    }

    #region Interface implementation

    public void AddObserver(string name, object sender = null)
    {
        throw new NotImplementedException();
    }

    public void PostNotification(string name, object data = null)
    {
        if(data != null)
            NotificationCenter.DefaultCenter().PostNotification(this, name, data);
        else
            NotificationCenter.DefaultCenter().PostNotification(this, name);
    }
    #endregion
}
