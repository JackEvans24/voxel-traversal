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
            cellObjects.Add(cell.Address, newCell);

            newCell.transform.position = new Vector2(cell.Address.x, cell.Address.y);
            newCell.SetGridCell(cell);
            UpdateGridCellUI(cell);
            newCell.Clicked += OnCellClicked;
        }

        public void UpdateGridCellUI(GridCell cell)
        {
            if (!cellObjects.TryGetValue(cell.Address, out var newCell))
            {
                Debug.LogError("Trying to update a cell that doesn't exist");
                return;
            }
            
            if (cell.IsHit)
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

        private void OnCellClicked(CellAddress cellAddress) => CellClicked?.Invoke(cellAddress);
    }
}
