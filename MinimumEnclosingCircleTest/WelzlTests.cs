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

    [Test]
    public void Circle2()
    {
        Vector2 point1 = new(0, 1);
        Vector2 point2 = new(0, -1);
        Vector2 point3 = new(1, 0);
        Vector2 point4 = new(0, 0.5f);
        Vector2 point5 = new(0, -0.5f);
        Vector2 point6 = new(0, 0);
        Vector2 point7 = new(0.25f, 0.25f);

        List<Vector2> points = [point1, point2, point3, point4, point5, point6, point7];

        var sut = Welzl.GetMinimumEnclosingCircle(points);

        sut.Should().NotBeNull();
        sut.Center.X.Should().Be(0);
        sut.Center.Y.Should().Be(0);
        sut.Radius.Should().Be(1);
        sut.PointIsInCircle(point1).Should().BeTrue();
        sut.PointIsInCircle(point2).Should().BeTrue();
        sut.PointIsInCircle(point3).Should().BeTrue();
        sut.PointIsInCircle(point4).Should().BeTrue();
        sut.PointIsInCircle(point5).Should().BeTrue();
        sut.PointIsInCircle(point6).Should().BeTrue();
        sut.PointIsInCircle(point7).Should().BeTrue();
    }

    [Test]
    public void Circle3()
    {
        Vector2 point1 = new(1, 1);
        Vector2 point2 = new(2, 4);
        Vector2 point3 = new(5, 3);
        Vector2 point4 = new(4, 2);

        List<Vector2> points = [point1, point2, point3, point4];

        var sut = Welzl.GetMinimumEnclosingCircle(points);

        sut.Should().NotBeNull();
        sut.Center.X.Should().Be(3);
        sut.Center.Y.Should().Be(2);
        Math.Round(sut.Radius, 2).Should().Be(2.24);
        sut.PointIsInCircle(point1).Should().BeTrue();
        sut.PointIsInCircle(point2).Should().BeTrue();
        sut.PointIsInCircle(point3).Should().BeTrue();
        sut.PointIsInCircle(point4).Should().BeTrue();
    }
}
