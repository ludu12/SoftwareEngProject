using UnityEngine;
using System.Collections;

public interface IMap{

    void DestroyThis(GameObject go);
    GameObject InstantiateGround(Vector3 startingPos, Quaternion startingRot);
    GameObject InstantiateGroundT(Vector3 startingPos, Quaternion startingRot);
    GameObject InstantiateBridgeU(Vector3 startingPos, Quaternion startingRot);
    GameObject InstantiateBridgeT(Vector3 startingPos, Quaternion startingRot);
    GameObject InstantiateBridgeF(Vector3 startingPos, Quaternion startingRot);
}
