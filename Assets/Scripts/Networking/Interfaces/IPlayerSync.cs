using UnityEngine;
using System.Collections;

public interface IPlayerSync
{
    Vector3 LerpPosition(Vector3 newPos, float lerpRate);
    Quaternion LerpRotation(Quaternion newRot, float lerpRate);
}
