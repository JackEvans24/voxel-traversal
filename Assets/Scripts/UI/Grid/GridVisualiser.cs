using System;
using System.Collections.Generic;
using TraversalDemo.Models;
using UnityEngine;

namespace TraversalDemo.UI.Grid
{
    public class GridVisualiser : MonoBehaviour
    {
        public event Action<CellAddress> CellClicked;

        [SerializeField] private GridCellUI gridCellPrefab;

        private readonly Dictionary<CellAddress, GridCellUI> cellObjects = new();

        public void CreateGridCellUI(GridCell cell)
        {
            if (cellObjects.TryGetValue(cell.Address, out _))
                return;

            var newCell = Instantiate(gridCellPrefab, transform);
            newCell.transform.position = new Vector2(cell.Address.x, cell.Address.y);
            newCell.SetGridCell(cell);
            newCell.ResetCellColour();
            newCell.Clicked += OnCellClicked;
            cellObjects.Add(cell.Address, newCell);
        }

        public void UpdateGridCellUI(GridCell cell, bool isHit)
        {
            if (!cellObjects.TryGetValue(cell.Address, out var newCell))
            {
                Debug.LogError("Trying to update a cell that doesn't exist");
                return;
            }
            
            if (isHit)
                newCell.SetHitCell();
            else
                newCell.ResetCellColour();
        }

        public void ClearCells()
        {
            foreach (var (_, cellUI) in cellObjects)
            {
                cellUI.Clicked -= OnCellClicked;
                Destroy(cellUI.gameObject);
            }
            cellObjects.Clear();
        }

        public void SetHitCell(CellAddress address)
        {
            if (!cellObjects.TryGetValue(address, out var cellObject))
            {
                Debug.LogWarning($"Unable to get cell at address: {address.x}, {address.y}");
                return;
            }

            cellObject.SetHitCell();
        }

        public void ResetCell(CellAddress address)
        {
            if (!cellObjects.TryGetValue(address, out var cellObject))
            {
                Debug.LogWarning($"Unable to get cell at address: {address.x}, {address.y}");
                return;
            }

            cellObject.ResetCellColour();
        }

        private void OnCellClicked(CellAddress cellAddress) => CellClicked?.Invoke(cellAddress);
    }
}
