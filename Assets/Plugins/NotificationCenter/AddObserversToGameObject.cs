using UnityEngine;
using System.Collections.Generic;

public class AddObserversToGameObject : MonoBehaviour {

	public List<string> messagesToObserve = new List<string>();

	/// <summary>
	/// This is a useful script of you have several scripts on a game object that all listen to for the same messages. Note, it expects you
	/// have one or more other scripts actually handing the messages.
	/// </summary>

	void Start () {
		foreach(string message in messagesToObserve)
		{
			NotificationCenter.DefaultCenter().AddObserver(this, message);
		}
	}

}
