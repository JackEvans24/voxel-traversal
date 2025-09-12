using TraversalDemo.Models;
using TraversalDemo.Services;
using TraversalDemo.UI.Grid;
using TraversalDemo.UI.Line;
using UnityEngine;

namespace TraversalDemo.Controller
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private CellAddress gridSize;

        private GridVisualiser gridVisualiser;
        private LineVisualiser lineVisualiser;
        private CameraController cameraController;

        private void Awake()
        {
            gridVisualiser = GetComponentInChildren<GridVisualiser>();
            lineVisualiser = GetComponentInChildren<LineVisualiser>();
            cameraController = gameObject.AddChildComponent<CameraController>();
        }

        private void Start()
        {
            var gridCells = GridGenerationService.GenerateGrid(gridSize);
            gridVisualiser.UpdateGridUI(gridCells);

            lineVisualiser.UpdateLine(new Line(Vector2.one * 1f, Vector2.one * 4.5f));
            
            cameraController.CenterCameraOnGrid(gridSize);
        }
    }
}
