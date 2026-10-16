using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MinimumEnclosingCircle;

public class Circle
{
    public Vector2 Center { get; set; }
    public double Radius { get; set; }

    /// <summary>
    /// Creates a circle with the specified center and radius
    /// </summary>
    public Circle(Vector2 center, double radius)
    {
        this.Center = center;
        this.Radius = radius;
    }

    /// <summary>
    /// Creates a circle given 2 to 3 points.
    /// </summary>
    public Circle(Vector2 point1, Vector2 point2, Vector3? point3 = null)
    {
        throw new NotImplementedException("Circle: point constructor not yet implemented");
    }


    /// <summary>
    /// determines if the specified point is within the bounds of the circle.
    /// Includes a buffer of 1e-9 to handle potential rounding errors.
    /// </summary>
    /// <param name="point">an X,Y coordinate</param>
    /// <returns>true if within the bounds of the circle, false if not</returns>
    public bool PointIsInCircle(Vector2 point)
    {
        var distance = Vector2.Distance(Center, point);
        var roundingBuffer = 1e-9;

        return distance <= this.Radius + roundingBuffer;
    }
}
