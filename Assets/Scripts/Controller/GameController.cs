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

        [Header("Collision")]
        [SerializeField] private GameObject collisionMarker;

        private LineVisualiser lineVisualiser;
        private CameraController cameraController;
        private GridController gridController;
        private CollisionController collisionController;

        private void Awake()
        {
            lineVisualiser = GetComponentInChildren<LineVisualiser>();
            cameraController = gameObject.AddChildComponent<CameraController>();
            
            var gridVisualiser = GetComponentInChildren<GridVisualiser>();
            gridController = new GridController(gridVisualiser);

            collisionController = new CollisionController(collisionMarker);

            lineVisualiser.LineHandlesUpdated += OnLineHandlesUpdated;
        }

        private void Start()
        {
            var gridCells = GridGenerationService.GenerateGrid(gridSize, walls);
            gridController.SetCells(gridCells);

            lineVisualiser.UpdateLine(line);
            
            cameraController.CenterCameraOnGrid(gridSize);

            SetHitCells();
        }

        private void OnLineHandlesUpdated(Vector3 lineStart, Vector3 lineEnd)
        {
            line.Start = lineStart;
            line.End = lineEnd;

            lineVisualiser.UpdateLine(line);

            SetHitCells();
        }

        private void SetHitCells()
        {
            gridController.ClearHitCells();
            collisionController.ResetCollision();

            var collisionOccurred = false;

            foreach (var hitCellData in VoxelTraversalService.TraverseRay(line))
            {
                gridController.SetHitCell(hitCellData.Position);

                if (collisionOccurred || !gridController.IsWall(hitCellData.Position))
                    continue;

                collisionOccurred = true;
                collisionController.UpdateCollision(line, hitCellData);
            }
        }
    }
}
