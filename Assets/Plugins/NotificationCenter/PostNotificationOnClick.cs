using UnityEngine;
using System.Collections;

public class PostNotificationOnClick : MonoBehaviour {

	public string message;

	void OnClick () {
		NotificationCenter.DefaultCenter().PostNotification(this, message);
	}
}
