using System.Collections.Generic;
using TraversalDemo.Models;
using TraversalDemo.UI.Grid;
using UnityEngine;

namespace TraversalDemo.Controller
{
    public class GridController
    {
        private readonly GridVisualiser gridVisualiser;

        private readonly Dictionary<CellAddress, GridCell> cellData = new();
        private readonly List<CellAddress> hitCells = new();

        public GridController(GridVisualiser gridVisualiser)
        {
            this.gridVisualiser = gridVisualiser;
        }

        public void SetCells(List<GridCell> cells)
        {
            gridVisualiser.ClearCells();

            foreach (var cell in cells)
            {
                if (cellData.TryGetValue(cell.Address, out _))
                    continue;
                cellData.Add(cell.Address, cell);

                gridVisualiser.CreateGridCellUI(cell);
            }
        }

        public void SetHitCell(CellAddress cell)
        {
            hitCells.Add(cell);
            gridVisualiser.SetHitCell(cell);
        }

        public bool IsWall(CellAddress address)
        {
            if (!cellData.TryGetValue(address, out var cell))
            {
                Debug.LogWarning($"Unable to get cell at address: {address.x}, {address.y}");
                return false;
            }

            return cell.IsWall;
        }

        public void ClearHitCells()
        {
            foreach (var cell in hitCells)
                gridVisualiser.ResetCell(cell);
            hitCells.Clear();
        }
    }
}
