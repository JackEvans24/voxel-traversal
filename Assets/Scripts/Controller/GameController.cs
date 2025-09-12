using TraversalDemo.Models;
using TraversalDemo.Services;
using TraversalDemo.UI.Grid;
using TraversalDemo.UI.Line;
using UnityEngine;

namespace TraversalDemo.Controller
{
    public class GameController : MonoBehaviour
    {
        [Header("Grid")]
        [SerializeField] private CellAddress gridSize;

        [Header("Line")]
        [SerializeField] private Line line;

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

            lineVisualiser.UpdateLine(line);
            
            cameraController.CenterCameraOnGrid(gridSize);

            foreach (var hitCellData in VoxelTraversalService.TraverseRay(line))
                gridVisualiser.SetHitCell(hitCellData.Position);
        }
    }
}
