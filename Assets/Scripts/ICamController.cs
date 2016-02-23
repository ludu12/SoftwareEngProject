using UnityEngine;
using System.Collections;

public interface ICamController{

    void SetPosition(Vector3 newPosition);

    void SetRotation(Vector3 eulerAngles);
}
