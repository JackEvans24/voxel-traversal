using System;
using UnityEngine;

namespace TraversalDemo.UI.Line
{
    public class LineHandleUI : MonoBehaviour
    {
        public event Action<Vector3> PositionUpdated;

        private Camera cam;

        private Vector2 offset;
        private bool isDragging;

        public void UpdatePosition(Vector3 newPosition)
        {
            if (isDragging) return;
            transform.position = newPosition;
        }

        private void Start()
        {
            cam = Camera.main;
        }

        private void OnMouseDown()
        {
            var localMousePosition = GetLocalMousePosition();
            offset = transform.position - localMousePosition;
            isDragging = true;
        }

        private void OnMouseDrag()
        {
            if (!isDragging)
                return;

            var localMousePosition = GetLocalMousePosition();
            transform.position = localMousePosition + (Vector3)offset;

            PositionUpdated?.Invoke(transform.position);
        }

        private void OnMouseUp()
        {
            isDragging = false;
        }

        private Vector3 GetLocalMousePosition()
        {
            var localMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            localMousePosition.z = 0;
            return localMousePosition;
        }
    }
}
