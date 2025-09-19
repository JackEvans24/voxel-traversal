using System.Collections.Generic;
using TraversalDemo.Models;
using UnityEngine;

namespace TraversalDemo.UI.Grid
{
    public class GridVisualiser : MonoBehaviour
    {
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
            cellObjects.Add(cell.Address, newCell);
        }

        public void ClearCells()
        {
            foreach (var (_, cellUI) in cellObjects)
                Destroy(cellUI.gameObject);
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
    }
}
