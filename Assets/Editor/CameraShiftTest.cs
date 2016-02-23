using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;


[TestFixture]
public class CameraShiftTest : MonoBehaviour
{
    Vector3 localPosition;
    Quaternion localRotation;
    Vector3 lookForwardPos;
    Vector3 lookForwardRot;
    Vector3 lookBackPos;
    Vector3 lookBackRot;
    Vector3 firstPersonPos;
    Vector3 firstPersonRot;

    CameraController camera;

    [Test]
    public void initializeValues()
    {
        camera = new CameraController();
        var cam = GetCamMock();
        camera.SetCameraController(cam);

        Vector3 localPosition = new Vector3(0, 0, 0);
        Quaternion localRotation = new Quaternion(0, 0, 0, 0);

        camera.assignStartValues(localPosition, localRotation);
        setValues();



        Assert.AreEqual(lookForwardPos, camera.lookForwardPos);
        Assert.AreEqual(lookForwardRot, camera.lookForwardRot);
        Assert.AreEqual(lookBackPos, camera.lookBackPos);
        Assert.AreEqual(lookBackRot, camera.lookBackRot);
        Assert.AreEqual(camera.fperson,false);
        Assert.AreEqual(firstPersonPos, camera.firstPersonPos);
        Assert.AreEqual(firstPersonRot, camera.firstPersonRot);

    }

    [Test]
    public void lookBack(){
        camera = new CameraController();
        var cam = GetCamMock();
        camera.SetCameraController(cam);

        camera.assignStartValues(new Vector3(0,0,0), new Quaternion(0,0,0,0));
        setValues();

        //Alt was pressed
        camera.leftShiftkey();

        cam.Received().SetPosition(lookBackPos);
        cam.Received().SetRotation(lookBackRot);
    }

    [Test]
    public void putBackCameraWhenInFirstPerson()
    {
        camera = new CameraController();
        var cam = GetCamMock();
        camera.SetCameraController(cam);

        camera.assignStartValues(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        setValues();
        camera.fperson = true;
        camera.putBackCamera();

        cam.Received().SetPosition(firstPersonPos);
        cam.Received().SetRotation(firstPersonRot);
    }

    [Test]
    public void putBackCameraWhenInThirdPerson()
    {
        camera = new CameraController();
        var cam = GetCamMock();
        camera.SetCameraController(cam);

        camera.assignStartValues(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        setValues();
        camera.fperson = false;
        camera.putBackCamera();

        cam.Received().SetPosition(lookForwardPos);
        cam.Received().SetRotation(lookForwardRot);
    }

    [Test]
    public void toggleCameraWhenInFirstPerson(){
        camera = new CameraController();
        var cam = GetCamMock();
        camera.SetCameraController(cam);

        camera.assignStartValues(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        setValues();
        camera.fperson = true;
        camera.leftAltkey();

        Assert.AreEqual(camera.fperson, false);
    }

    [Test]
    public void toggleCameraWhenInThirdPerson()
    {
        
        camera = new CameraController();
        var cam = GetCamMock();
        camera.SetCameraController(cam);

        camera.assignStartValues(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        setValues();
        camera.fperson = false;
        camera.leftAltkey();

        Assert.AreEqual(camera.fperson,true);
    }

    private ICamController GetCamMock()
    {
        return Substitute.For<ICamController>();
    }

    private void setValues()
    {
        localPosition = new Vector3(0, 0, 0);
        localRotation = new Quaternion(0, 0, 0, 0);

        //set lookforward
        lookForwardPos = localPosition;
        lookForwardRot = localRotation.eulerAngles;

        //set lookback
        lookBackPos = localPosition;
        lookBackPos.z = lookBackPos.z + camera.distance;
        lookBackRot = localRotation.eulerAngles;
        lookBackRot.y = lookBackRot.y + 180;

        //set first person
        firstPersonPos = localPosition;
        firstPersonPos.z += 3.5f;
        firstPersonPos.y -= camera.drop;
        firstPersonRot = localRotation.eulerAngles;
    }
}
