using System.Collections.Generic;
using TraversalDemo.Data;
using UnityEngine;

namespace TraversalDemo.UI.Grid
{
    public class GridVisualiser : MonoBehaviour
    {
        [SerializeField] private GameObject gridCellPrefab;

        private readonly Dictionary<CellAddress, GameObject> cellObjects = new();

        public void UpdateGridUI(List<GridCell> cells)
        {
            foreach (var cell in cells)
            {
                if (cellObjects.TryGetValue(cell.Address, out _))
                    continue;

                var newCell = Instantiate(gridCellPrefab, transform);
                newCell.transform.position = new Vector2(cell.Address.x, cell.Address.y);
                cellObjects.Add(cell.Address, newCell);
            }
        }
    }
}