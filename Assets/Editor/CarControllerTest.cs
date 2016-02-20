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
    }

    [Test]
    public void Right()
    {

    }
}
