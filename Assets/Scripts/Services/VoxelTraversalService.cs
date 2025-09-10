using System.Collections.Generic;
using TraversalDemo.Data;
using UnityEngine;

namespace TraversalDemo.Services
{
    public class VoxelTraversalService
    {
        public static IEnumerable<VoxelTraversalData> TraverseRay(Line line)
        {
            var start = line.Start;
            var end = line.End;

            // Current position in grid coordinates
            var currentGridX = (int)Mathf.Floor(start.x);
            var currentGridY = (int)Mathf.Floor(start.y);

            var endX = (int)Mathf.Floor(end.x);
            var endY = (int)Mathf.Floor(end.y);

            // Ray direction
            var rayDir = end - start;

            // Calculate delta distances
            var deltaDistX = rayDir.x == 0 ? float.MaxValue : Mathf.Abs(1.0f / rayDir.x);
            var deltaDistY = rayDir.y == 0 ? float.MaxValue : Mathf.Abs(1.0f / rayDir.y);

            // Calculate step and initial sideDist
            float sideDistX, sideDistY;
            int stepX, stepY;

            if (rayDir.x < 0)
            {
                stepX = -1;
                sideDistX = (start.x - currentGridX) * deltaDistX;
            }
            else
            {
                stepX = 1;
                sideDistX = (currentGridX + 1.0f - start.x) * deltaDistX;
            }

            if (rayDir.y < 0)
            {
                stepY = -1;
                sideDistY = (start.y - currentGridY) * deltaDistY;
            }
            else
            {
                stepY = 1;
                sideDistY = (currentGridY + 1.0f - start.y) * deltaDistY;
            }

            // Perform DDA traversal
            while (currentGridX != endX || currentGridY != endY)
            {
                Direction direction;

                // Jump to next cell
                if (sideDistX < sideDistY)
                {
                    sideDistX += deltaDistX;
                    currentGridX += stepX;
                    direction = stepX > 0 ? Direction.WEST : Direction.EAST;
                }
                else
                {
                    sideDistY += deltaDistY;
                    currentGridY += stepY;
                    direction = stepY > 0 ? Direction.SOUTH : Direction.NORTH;
                }

                var cell = new Vector2(currentGridX, currentGridY);
                yield return new VoxelTraversalData(cell, direction);
            }
        }
    }
}
