namespace MinimumEnclosingCircleTest;

using MinimumEnclosingCircle;
using NUnit;
using AwesomeAssertions;
using System.Numerics;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

public class WelzlTests
{
    [Test]
    public void Circle1()
    {
        Vector2 point1 = new(0, 1);
        Vector2 point2 = new(0, -1);
        Vector2 point3 = new(1, 0);
        Vector2 point4 = new(0, 0.5f);

        List<Vector2> points = [point1, point2, point3, point4];

        var sut = Welzl.GetMinimumEnclosingCircle(points);

        sut.Should().NotBeNull();
        sut.Center.X.Should().Be(0);
        sut.Center.Y.Should().Be(0);
        sut.Radius.Should().Be(1);
        sut.PointIsInCircle(point1).Should().BeTrue();
        sut.PointIsInCircle(point2).Should().BeTrue();
        sut.PointIsInCircle(point3).Should().BeTrue();
        sut.PointIsInCircle(point4).Should().BeTrue();
    }
}
