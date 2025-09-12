using System.Collections.Generic;
using TraversalDemo.Models;

namespace TraversalDemo.Services
{
    public static class GridGenerationService
    {
        public static List<GridCell> GenerateGrid(CellAddress gridSize)
        {
            var result = new List<GridCell>();
            var currentCell = new CellAddress(0, 0);

            for (int x = 0; x < gridSize.x; x++)
            {
                currentCell.x = x;
                for (int y = 0; y < gridSize.y; y++)
                {
                    currentCell.y = y;
                    var cell = new GridCell { Address = currentCell, IsWall = false };
                    result.Add(cell);
                }
            }

            return result;
        }
    }
}