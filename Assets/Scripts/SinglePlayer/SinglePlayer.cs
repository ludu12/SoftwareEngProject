using UnityEngine;
using System.Collections;

public class SinglePlayer {

    // Score of the player
    private int _score = 0;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    // The new threshold were we go to a new level
    private int _newLevelThreshold = 200;
    public int NewLevelThreshold
    {
        get
        {
            return _newLevelThreshold;
        }
        set
        {
            _newLevelThreshold = value;
        }
    }

    // The amount which we increase the score each time
    public int _pointValue = 10;
    public int PointValue
    {
        get
        {
            return _pointValue;
        }
        set
        {
            _pointValue = value;
        }
    }

    INotificationCenter notificationInterface;

    // Timer variables
    private string minutes;
    private string seconds;
    private float timer = 0;

    /// <summary>
    /// Increases our score, post notification if we increase our level
    /// </summary>
    /// <returns></returns>
    public string ScoreIncrease()
    {
        Score += PointValue;
        if (Score % NewLevelThreshold == 0)
            notificationInterface.PostNotification("IncreaseLevel");

        return Score.ToString();
    }

    /// <summary>
    /// Returns the current time as a string in a nice format
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public string OnTimer(float deltaTime)
    {
        timer += deltaTime;

        minutes = Mathf.Floor(timer / 60).ToString("00");
        seconds = Mathf.Floor(timer % 60).ToString("00");

        return minutes + ":" + seconds;
    }

    /// <summary>
    /// Checks the angle between the two vectors to see if we are facing the wrong way
    /// </summary>
    /// <param name="mapDirection"></param>
    /// <param name="myDireciton"></param>
    /// <returns></returns>
    public bool IsFacingCorrectWay(Vector2 mapDirection, Vector2 myDireciton)
    {
        Debug.Log(Vector3.Angle(mapDirection, myDireciton));

        if (Vector3.Angle(mapDirection, myDireciton) < 150)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Set our notification interface so we can post messages
    /// </summary>
    /// <param name="notificationInterface"></param>
    public void SetNotificationInterface(INotificationCenter notificationInterface)
    {
        this.notificationInterface = notificationInterface;
    }
}
