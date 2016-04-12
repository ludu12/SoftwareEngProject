using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class PostInputNotificationUGUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDragHandler, IEndDragHandler
, IPointerEnterHandler, IPointerExitHandler, IScrollHandler
{
    public string appendMessageWith = "";
    public bool verbose = false;

    public void OnDrag(PointerEventData eventData)
    {
        string message = "Drag" + appendMessageWith;
        NotificationCenter.DefaultCenter().PostNotification(this, message, eventData);

        if (verbose) Debug.Log(message);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        string message = "EndDrag" + appendMessageWith;
        NotificationCenter.DefaultCenter().PostNotification(this, message, eventData);
    }

    public void OnScroll(PointerEventData eventData)
    {
        string message = "Scroll" + appendMessageWith;
        NotificationCenter.DefaultCenter().PostNotification(this, message, eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string message = "PointerEnter" + appendMessageWith;
        NotificationCenter.DefaultCenter().PostNotification(this, message, eventData);

        if (verbose) Debug.Log(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        string message = "PointerExit" + appendMessageWith;
        NotificationCenter.DefaultCenter().PostNotification(this, message, eventData);

        if (verbose) Debug.Log(message);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string message = "Click" + appendMessageWith;
        NotificationCenter.DefaultCenter().PostNotification(this, message, eventData);

        if (verbose) Debug.Log(message);
    }

    public void OnPointerDown(PointerEventData eventData)
    { 
        string message = "PointerDown" + appendMessageWith;
        NotificationCenter.DefaultCenter().PostNotification(this, message, eventData);

        if (verbose) Debug.Log(message);
    }

    public void OnPointerUp(PointerEventData eventData)
    { 
        string message = "PointerUp" + appendMessageWith;
        NotificationCenter.DefaultCenter().PostNotification(this, message, eventData);

        if (verbose) Debug.Log(message);
    }
}
