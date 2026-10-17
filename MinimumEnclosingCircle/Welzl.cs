using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MinimumEnclosingCircle;

/// <summary>
/// Welzl's algorithm to find a minimum enclosing circle
/// </summary>
public static class Welzl
{
    /// <summary>
    /// Finds the minimum enclosing circle given a set of points.
    /// </summary>
    /// <param name="points">the points in a graph</param>
    /// <returns>minimum enclosing circle or null if none found</returns>
    public static Circle? GetMinimumEnclosingCircle(List<Vector2> points)
    {
        if (points.Count <= 3) return CreateCircle(points);

        List<Vector2> shuffled = points.Shuffle().ToList();

        return GetMinimumEnclosingCircle(shuffled, [], shuffled.Count);
    }

    private static Circle? GetMinimumEnclosingCircle(List<Vector2> points, List<Vector2> edge, int remainingPoints)
    {
        if (remainingPoints == 0 || edge.Count == 3) return CreateCircle(edge);

        Vector2 point  = points[remainingPoints - 1];
        Circle? circle = GetMinimumEnclosingCircle(points, edge, remainingPoints - 1);

        if (circle is not null && circle.PointIsInCircle(point)) return circle;

        edge.Add(point);

        circle = GetMinimumEnclosingCircle(points, edge, remainingPoints - 1);

        edge.RemoveAt(edge.Count - 1);

        return circle;
    }

    /// <summary>
    /// Creates a circle given the specified points
    /// </summary>
    /// <param name="points">points with which to create a circle</param>
    /// <returns>circle, or null if circle can't be created</returns>
    private static Circle? CreateCircle(List<Vector2> points)
    {
        switch (points.Count)
        {
            case 2:
                return new(points[0], points[1]);

            case 3:
                return new(points[0], points[1], points[2]);

            default:
                return null;
        }
    }
}
