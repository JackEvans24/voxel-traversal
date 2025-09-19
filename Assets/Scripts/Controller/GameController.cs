using System.Collections.Generic;
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
        [SerializeField] private List<CellAddress> walls;

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

            lineVisualiser.LineHandlesUpdated += OnLineHandlesUpdated;
        }

        private void Start()
        {
            var gridCells = GridGenerationService.GenerateGrid(gridSize, walls);
            gridVisualiser.UpdateGridUI(gridCells);

            lineVisualiser.UpdateLine(line);
            
            cameraController.CenterCameraOnGrid(gridSize);

            foreach (var hitCellData in VoxelTraversalService.TraverseRay(line))
                gridVisualiser.SetHitCell(hitCellData.Position);
        }

        private void OnLineHandlesUpdated(Vector3 lineStart, Vector3 lineEnd)
        {
            line.Start = lineStart;
            line.End = lineEnd;

            lineVisualiser.UpdateLine(line);
            
            gridVisualiser.ClearHitCells();
            foreach (var hitCellData in VoxelTraversalService.TraverseRay(line))
                gridVisualiser.SetHitCell(hitCellData.Position);
        }
    }
}
