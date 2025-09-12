using TraversalDemo.Data;
using TraversalDemo.Services;
using TraversalDemo.UI.Grid;
using UnityEngine;

namespace TraversalDemo.Controller
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private CellAddress gridSize;

        private GridVisualiser gridVisualiser;
        private CameraController cameraController;

        private void Awake()
        {
            gridVisualiser = GetComponentInChildren<GridVisualiser>();
            cameraController = AddChildComponent<CameraController>();
        }

        private void Start()
        {
            var gridCells = GridGenerationService.GenerateGrid(gridSize);
            gridVisualiser.UpdateGridUI(gridCells);
            
            cameraController.CenterCameraOnGrid(gridSize);
        }

        private T AddChildComponent<T>() where T : MonoBehaviour
        {
            var childGameObject = new GameObject(typeof(T).Name);
            childGameObject.transform.parent = transform;

            var component = childGameObject.AddComponent<T>();
            return component;
        }
    }
}