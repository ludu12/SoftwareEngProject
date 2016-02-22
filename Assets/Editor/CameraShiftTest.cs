using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class CameraShiftTest : MonoBehaviour
{
    [Test]
    public void SwitchToFirstPerson(){
        //create instance of CameraShift and set to 3rd person
        var camera = new CameraShift();
        camera.fperson = false;

        //Alt was pressed
        camera.leftAltkey();

        // assert (verify) fperson is true
        Assert.AreEqual(true, camera.fperson);
    }

    [Test]
    public void lookBack(){
        //create instance of CameraShift
        var camera = new CameraShift();
        camera.lookForwardPos = new Vector3 (0f,1.5f,-2.5f);

        //camera.assignStartValues();

        //Alt was pressed
        camera.leftShiftkey();

        //assert (verify) camera is in lookBack Position and Rotation
        Assert.AreNotEqual(camera.transform.localPosition, camera.lookForwardPos);
        //Assert.AreEqual(camera.transform.localRotation, Quaternion.Euler(camera.lookBackRot));        
    }

    [Test]
    public void undoLookBack(){
        //create instance of CameraShift
        var camera = new CameraShift();
        camera.assignStartValues();

        //Alt was pressed
        camera.leftShiftkey();
        camera.putBackCamera();

        //assert (verify) camera is in lookBack Position and Rotation
        //Assert.AreEqual(camera.transform.localPosition, camera.lookForwardPos);
        //Assert.AreEqual(camera.transform.localRotation, Quaternion.Euler(lookForwardRot));        
    }
}
