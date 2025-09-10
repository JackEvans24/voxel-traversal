using System;
using TraversalDemo.Data;
using UnityEngine;

namespace TraversalDemo.Services
{
    public class RayIntersectionService
    {
        public static Vector2? GetIntersection(Line line, Vector2 gridCell, Direction hitDirection)
        {
            var cellWall = GetCellWall(gridCell, hitDirection);
            return GetIntersection(line, cellWall);
        }

        private static Line GetCellWall(Vector2 gridCell, Direction wall)
        {
            return wall switch
            {
                Direction.NORTH => new Line(gridCell + Vector2.up, gridCell + Vector2.one),
                Direction.EAST => new Line(gridCell + Vector2.right, gridCell + Vector2.one),
                Direction.SOUTH => new Line(gridCell, gridCell + Vector2.right),
                Direction.WEST => new Line(gridCell, gridCell + Vector2.up),
                _ => throw new ArgumentOutOfRangeException(nameof(wall), wall, null)
            };
        }

        private static Vector2? GetIntersection(Line line1, Line line2)
        {
            Vector2 p1 = line1.Start;
            Vector2 p2 = line1.End;
            Vector2 p3 = line2.Start;
            Vector2 p4 = line2.End;
        
            Vector2 d1 = p2 - p1; // Direction vector of ray1
            Vector2 d2 = p4 - p3; // Direction vector of ray2
        
            // Calculate the denominator for the parametric equation
            float denominator = d1.x * d2.y - d1.y * d2.x;
            if (Mathf.Approximately(denominator, 0f)) return null;
            
            // Calculate parameters for intersection point
            Vector2 p1p3 = p3 - p1;
            float t1 = (p1p3.x * d2.y - p1p3.y * d2.x) / denominator;
            float t2 = (p1p3.x * d1.y - p1p3.y * d1.x) / denominator;
        
            // Check if intersection occurs within both line segments
            if (t1 >= 0f && t1 <= 1f && t2 >= 0f && t2 <= 1f)
            {
                // Calculate intersection point
                return p1 + d1 * t1;
            }
        
            // Lines intersect, but not within the segments
            return null;
        }
    }
}