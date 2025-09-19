using TraversalDemo.Models;
using UnityEngine;

namespace TraversalDemo.UI.Grid
{
    public class GridCellUI : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private GridCell gridCell;

        private readonly Color DEFAULT_CELL_COLOR = Color.white;
        private readonly Color DEFAULT_WALL_COLOR = new(0.45f, 0.25f, 0f);
        private readonly Color HIT_CELL_COLOR = new(0.85f, 0.4f, 0.4f);
        private readonly Color HIT_WALL_COLOR = new(0.55f, 0.45f, 0f);

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetGridCell(GridCell cell) => gridCell = cell;

        public void SetHitCell() => spriteRenderer.color = gridCell.IsWall ? HIT_WALL_COLOR : HIT_CELL_COLOR;

        public void ResetCellColour() =>
            spriteRenderer.color = gridCell.IsWall ? DEFAULT_WALL_COLOR : DEFAULT_CELL_COLOR;
    }
}
