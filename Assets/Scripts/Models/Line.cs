using System;
using UnityEngine;

namespace TraversalDemo.Models
{
    [Serializable]
    public struct Line
    {
        [SerializeField] public Vector2 Start;
        [SerializeField] public Vector2 End;

        public Line(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        public Vector3[] GetPositions() => new Vector3[] { Start, End };
    }
}
