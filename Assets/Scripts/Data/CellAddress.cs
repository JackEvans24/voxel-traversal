using System;
using UnityEngine;

namespace TraversalDemo.Data
{
    [Serializable]
    public struct CellAddress : IEquatable<CellAddress>
    {
        [SerializeField] public int x;
        [SerializeField] public int y;

        public CellAddress(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(CellAddress other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            return obj is CellAddress other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}