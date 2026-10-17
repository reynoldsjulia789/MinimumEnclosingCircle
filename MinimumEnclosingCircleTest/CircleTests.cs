namespace MinimumEnclosingCircleTest;

using NUnit;
using MinimumEnclosingCircle;
using AwesomeAssertions;
using System.Numerics;

public class CircleTests
{
    [Test]
    public void CreateCircle3Points()
    {
        // circle 1
        Vector2 point1 = new(1, 1);
        Vector2 point2 = new(2, 4);
        Vector2 point3 = new(5, 3);

        var sut = new Circle(point1, point2, point3);

        sut.Center.X.Should().Be(3);
        sut.Center.Y.Should().Be(2);
        Math.Round(sut.Radius, 2).Should().Be(2.24);

        // circle 2
        point1 = new(0, 1);
        point2 = new(0, -1);
        point3 = new(1, 0);

        sut = new Circle(point1, point2, point3);

        sut.Center.X.Should().Be(0);
        sut.Center.Y.Should().Be(0);
        sut.Radius.Should().Be(1);
    }

    [Test]
    public void CreateCircle2Points()
    {
        Vector2 point1 = new(0, 0);
        Vector2 point2 = new(0, 2);

        var sut = new Circle(point1, point2);

        sut.Center.X.Should().Be(0);
        sut.Center.Y.Should().Be(1);
        sut.Radius.Should().Be(1);
    }

    [Test]
    public void PointInCircle()
    {
        var sut = new Circle(new(0, 0), 2);

        sut.PointIsInCircle(new(0, 1)).Should().BeTrue();
        sut.PointIsInCircle(new(0, 2)).Should().BeTrue();
        sut.PointIsInCircle(new(0, 2.01f)).Should().BeFalse();
    }
}
