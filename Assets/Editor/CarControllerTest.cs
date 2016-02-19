using System;
using UnityEngine;
using NUnit.Framework;

[TestFixture] 
public class CarControllerTest
{
    public CarController GetController()
    {
        return NSubstitute.Substitute.For<CarController>();
    }
    [Test]
    public void Forward()
    {
        var controller = GetController();
        controller.OnForward();

        //Assert.That(controller.speed > 0);
        //Assert.That(controller.transform.position != controller.oldPos);
    }

    [Test]
    public void Right()
    {
        var controller = GetController();
        controller.OnRight();
    }
}
