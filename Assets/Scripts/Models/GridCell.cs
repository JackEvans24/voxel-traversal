namespace TraversalDemo.Models
{
    public struct GridCell
    {
        public CellAddress Address { get; set; }
        public bool IsWall { get; set; }
    }
}