using UnityEngine;

namespace TraversalDemo.UI.Grid
{
    public class GridCellUI : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetCellColour(Color color) => spriteRenderer.color = color;
    }
}
