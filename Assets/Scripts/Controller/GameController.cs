using System.Collections.Generic;
using System.Linq;
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

        private IEnumerable<VoxelTraversalData> cachedHitCells;

        private void Awake()
        {
            lineVisualiser = GetComponentInChildren<LineVisualiser>();
            cameraController = gameObject.AddChildComponent<CameraController>();
            
            var gridVisualiser = GetComponentInChildren<GridVisualiser>();
            gridController = new GridController(gridVisualiser);

            collisionController = new CollisionController(collisionMarker);

            lineVisualiser.LineHandlesUpdated += OnLineHandlesUpdated;
            gridController.CellsUpdated += OnCellsUpdated;
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

        private void OnCellsUpdated() => SetHitCells(recalculateHitData: false);

        private void SetHitCells(bool recalculateHitData = true)
        {
            collisionController.ResetCollision();

            if (recalculateHitData)
            {
                var hitCells = VoxelTraversalService.TraverseRay(line);
                var voxelTraversalDataList = hitCells.ToList();
                cachedHitCells = voxelTraversalDataList;
            }

            gridController.SetHitCells(cachedHitCells.Select(cell => cell.Position));

            foreach (var hitCellData in cachedHitCells)
            {
                if (!gridController.IsWall(hitCellData.Position))
                    continue;

                collisionController.UpdateCollision(line, hitCellData);
                break;
            }
        }
    }
}
