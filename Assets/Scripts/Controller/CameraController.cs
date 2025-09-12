using TraversalDemo.Models;
using UnityEngine;

namespace TraversalDemo.Controller
{
    public class CameraController : MonoBehaviour
    {
        private const float CameraZPosition = -10f;

        public void CenterCameraOnGrid(CellAddress gridSize)
        {
            var cam = Camera.main;
            cam.transform.position = new Vector3(gridSize.x / 2f, gridSize.y / 2f, CameraZPosition);
        }
    }
}
