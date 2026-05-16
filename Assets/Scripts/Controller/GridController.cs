using System;
using System.Collections.Generic;
using System.Linq;
using TraversalDemo.Models;
using TraversalDemo.UI.Grid;
using UnityEngine;

namespace TraversalDemo.Controller
{
    public class GridController
    {
        public event Action CellsUpdated;

        private readonly GridVisualiser gridVisualiser;

        private readonly Dictionary<CellAddress, GridCell> cellData = new();
        private List<CellAddress> hitCells = new();

        public GridController(GridVisualiser gridVisualiser)
        {
            this.gridVisualiser = gridVisualiser;
            this.gridVisualiser.CellClicked += OnCellClicked;
        }

        public void SetCells(List<GridCell> cells)
        {
            gridVisualiser.ClearCells();

            foreach (var cell in cells)
            {
                cellData.Remove(cell.Address, out _);
                cellData.Add(cell.Address, cell);

                gridVisualiser.CreateGridCellUI(cell);
            }
        }

        public void SetHitCells(IEnumerable<CellAddress> cells)
        {
            var cellAddresses = cells.ToList();

            // Reset cells no longer hit
            foreach (var hitCell in hitCells.Except(cellAddresses))
            {
                if (!cellData.TryGetValue(hitCell, out var cell)) continue;
                cell.IsHit = false;
                gridVisualiser.UpdateGridCellUI(cell);
            }

            // Mark newly hit cells
            foreach (var cellAddress in cellAddresses.Except(hitCells))
            {
                if (!cellData.TryGetValue(cellAddress, out var cell)) continue;
                cell.IsHit = true;
                gridVisualiser.UpdateGridCellUI(cell);
            }

            hitCells = cellAddresses;
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

        private void OnCellClicked(CellAddress cellAddress)
        {
            if (!cellData.TryGetValue(cellAddress, out var cell))
                Debug.LogError($"Trying to click cell that doesn't exist: {cellAddress}");

            cell.IsWall = !cell.IsWall;

            gridVisualiser.UpdateGridCellUI(cell);

            CellsUpdated?.Invoke();
        }
    }
}
