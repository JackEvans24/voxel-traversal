using UnityEngine;

namespace TraversalDemo.UI.Line
{
    public class LineVisualiser : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        public void UpdateLine(Models.Line line)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(line.GetPositions());
        }
    }
}