using System;
using System.Collections.Generic;
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
        private readonly List<CellAddress> hitCells = new();

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

        public void SetHitCell(CellAddress cell)
        {
            if (!cellData.ContainsKey(cell)) return;
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

        private void OnCellClicked(CellAddress cellAddress)
        {
            if (!cellData.TryGetValue(cellAddress, out var cell))
                Debug.LogError($"Trying to click cell that doesn't exist: {cellAddress}");

            cell.IsWall = !cell.IsWall;

            gridVisualiser.UpdateGridCellUI(cell, hitCells.Contains(cellAddress));

            CellsUpdated?.Invoke();
        }
    }
}
