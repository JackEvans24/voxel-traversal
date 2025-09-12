using UnityEngine;

namespace TraversalDemo.Data
{
    public struct VoxelTraversalData
    {
        public CellAddress Position;
        public Direction HitDirection;

        public VoxelTraversalData(CellAddress position, Direction hitDirection)
        {
            Position = position;
            HitDirection = hitDirection;
        }
    }
}