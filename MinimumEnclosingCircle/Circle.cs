using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace MinimumEnclosingCircle;

public class Circle
{
    public Vector2 Center { get; set; }
    public double Radius { get; set; }

    private static readonly double RoundingBuffer = 1e-9;

    /// <summary>
    /// Creates a circle with the specified center and radius
    /// </summary>
    public Circle(Vector2 center, double radius)
    {
        InitCircle(center, radius);
    }

    /// <summary>
    /// Creates a circle given 2 or 3 points
    /// </summary>
    public Circle(Vector2 point1, Vector2 point2, Vector2? point3 = null)
    {
        float centerX, centerY, radius;

        if (point3 == null)
        {
            // 2 point circle
            radius  = Vector2.Distance(point1, point2) / 2;
            centerX = (point1.X + point2.X) / 2;
            centerY = (point1.Y + point2.Y) / 2;

            InitCircle(new(centerX, centerY), radius);
            return;
        }

        float determinant = 2 * (point1.X * (point2.Y - point3.Value.Y) 
            + point2.X * (point3.Value.Y - point1.Y) 
            + point3.Value.X * (point1.Y - point2.Y));

        if (Math.Abs(determinant) <= RoundingBuffer)
        {
            // points are in a straight line and can't create a circle
            throw new ArgumentException("Circle constructor: cannot create circle from line");
        }

        var point1Squared = (point1.X * point1.X) + (point1.Y * point1.Y);
        var point2Squared = (point2.X * point2.X) + (point2.Y * point2.Y);
        var point3Squared = (point3.Value.X * point3.Value.X) + (point3.Value.Y * point3.Value.Y);

        centerX = (point1Squared * (point2.Y - point3.Value.Y) 
            + point2Squared * (point3.Value.Y - point1.Y)
            + point3Squared * (point1.Y - point2.Y))
            / determinant;

        centerY = (point1Squared * (point3.Value.X - point2.X)
            + point2Squared * (point1.X - point3.Value.X)
            + point3Squared * (point2.X - point1.X))
            / determinant;

        Vector2 center = new(centerX, centerY);

        radius = Vector2.Distance(center, point1);

        InitCircle(center, radius);
    }


    /// <summary>
    /// Initializes the circle with the specified center and radius
    /// </summary>
    /// <param name="center">Center point of the circle</param>
    /// <param name="radius">Radius of the circle; must be greater than zero</param>
    /// <exception cref="ArgumentException">Thrown if radius is less than or equal to zero</exception>
    private void InitCircle(Vector2 center, double radius)
    {
        if (radius <= 0) throw new ArgumentException("Circle constructor: circle cannot have negative radius.");

        this.Center = center;
        this.Radius = radius;
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

        return distance <= this.Radius + RoundingBuffer;
    }
}
