using System.Collections.Generic;
using TraversalDemo.Models;
using UnityEngine;

namespace TraversalDemo.UI.Grid
{
    public class GridVisualiser : MonoBehaviour
    {
        [SerializeField] private GridCellUI gridCellPrefab;

        private readonly Dictionary<CellAddress, GridCellUI> cellObjects = new();
        private readonly List<GridCellUI> hitCells = new();

        public void UpdateGridUI(List<GridCell> cells)
        {
            foreach (var cell in cells)
            {
                if (cellObjects.TryGetValue(cell.Address, out _))
                    continue;

                var newCell = Instantiate(gridCellPrefab, transform);
                newCell.transform.position = new Vector2(cell.Address.x, cell.Address.y);
                newCell.SetGridCell(cell);
                newCell.ResetCellColour();
                cellObjects.Add(cell.Address, newCell);
            }
        }

        public void SetHitCell(CellAddress address)
        {
            if (!cellObjects.TryGetValue(address, out var cell))
            {
                Debug.LogWarning($"Unable to get cell at address: {address.x}, {address.y}");
                return;
            }
            
            cell.SetHitCell();
            
            hitCells.Add(cell);
        }

        public void ClearHitCells()
        {
            foreach (var cell in hitCells)
                cell.ResetCellColour();
            hitCells.Clear();
        }
    }
}
