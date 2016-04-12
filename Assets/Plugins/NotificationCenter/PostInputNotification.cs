using UnityEngine;
using System.Collections;

public class PostInputNotification : MonoBehaviour {
	
	public string appendMessageWith = "";
	public bool verbose = false;
	
	public bool isPress{get; set;}
	public Vector2 dragDelta{get;set;}
	public float scrollVal{get;set;}
	public bool showTooltip{get; set;}
	
	public virtual void OnDrag(Vector2 delta) {
		dragDelta = delta;
		string message = "Drag" + appendMessageWith;
		NotificationCenter.DefaultCenter().PostNotification(this, message, delta);
		
		if(verbose) Debug.Log(message);
	}
	
	public virtual void OnClick() {
		string message = "Click" + appendMessageWith;
		NotificationCenter.DefaultCenter().PostNotification(this, message);
		
		if(verbose) Debug.Log(message);
	}
	
	public virtual void OnPress(bool isPress_i) {
		isPress = isPress_i;

		string message = "Press" + appendMessageWith;
		NotificationCenter.DefaultCenter().PostNotification(this, message, isPress);
		
		if(verbose) Debug.Log(message);
	}
	
	public virtual void OnDoubleClick()
	{
		string message = "DoubleClick" + appendMessageWith;
		NotificationCenter.DefaultCenter().PostNotification(this, message);
	}
	
	public virtual void OnTooltip(bool show) {

		showTooltip = show;

		string message = "Tooltip" + appendMessageWith;
		NotificationCenter.DefaultCenter().PostNotification(this, message, show);
		
		if(verbose) Debug.Log(message);
	}
	
	public virtual void OnScroll(float delta) {
		scrollVal = delta;
		
		string message = "Scroll" + appendMessageWith;
		NotificationCenter.DefaultCenter().PostNotification(this, message, delta);
		
		if(verbose) Debug.Log(message);
	}

}
