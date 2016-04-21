using UnityEngine;
using System.Collections;

public interface IDestroyInstantiate{

    void DestroyThis(GameObject go);
    GameObject InstantiateGameObject(GameObject go, Vector3 startingPos, Quaternion startingRot);

}
