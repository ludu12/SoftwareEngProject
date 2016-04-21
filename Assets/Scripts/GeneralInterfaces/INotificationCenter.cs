using UnityEngine;
using System.Collections;

public interface INotificationCenter {

    void AddObserver(string name, object sender = null);
    void PostNotification(string name, object data = null);

}
