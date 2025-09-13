using System;
using UnityEngine;

namespace TraversalDemo.UI.Line
{
    public class LineVisualiser : MonoBehaviour
    {
        public event Action<Vector3, Vector3> LineHandlesUpdated;

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private LineHandleUI lineHandlePrefab;

        private LineHandleUI lineStart;
        private LineHandleUI lineEnd;

        private void Awake()
        {
            lineStart = Instantiate(lineHandlePrefab);
            lineStart.PositionUpdated += OnLineStartPositionUpdated;

            lineEnd = Instantiate(lineHandlePrefab);
            lineEnd.PositionUpdated += OnLineEndPositionUpdated;
        }

        private void OnLineStartPositionUpdated(Vector3 lineStartPosition) =>
            LineHandlesUpdated?.Invoke(lineStartPosition, lineEnd.transform.position);

        private void OnLineEndPositionUpdated(Vector3 lineEndPosition) =>
            LineHandlesUpdated?.Invoke(lineStart.transform.position, lineEndPosition);

        public void UpdateLine(Models.Line line)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(line.GetPositions());

            lineStart.UpdatePosition(line.Start);
            lineEnd.UpdatePosition(line.End);
        }
    }
}
