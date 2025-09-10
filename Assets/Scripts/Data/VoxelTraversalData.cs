using UnityEngine;

namespace TraversalDemo.Data
{
    public struct VoxelTraversalData
    {
        public Vector2 Position;
        public Direction HitDirection;

        public VoxelTraversalData(Vector2 position, Direction hitDirection)
        {
            Position = position;
            HitDirection = hitDirection;
        }
    }
}