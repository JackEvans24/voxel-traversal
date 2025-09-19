using TraversalDemo.Models;
using TraversalDemo.Services;
using UnityEngine;

namespace TraversalDemo.Controller
{
    public class CollisionController
    {
        private readonly GameObject collisionMarker;

        public CollisionController(GameObject collisionMarker)
        {
            this.collisionMarker = collisionMarker;
        }

        public void ResetCollision()
        {
            collisionMarker.SetActive(false);
        }

        public void UpdateCollision(Line line, VoxelTraversalData hitCellData)
        {
            var collision = RayIntersectionService.GetIntersection(
                line,
                hitCellData.Position,
                hitCellData.HitDirection
            );
            
            if (!collision.HasValue)
            {
                Debug.LogWarning($"Unable to find collision between line and cell {hitCellData.Position}");
                return;
            }

            collisionMarker.transform.position = collision.Value;
            collisionMarker.SetActive(true);
        }
    }
}
