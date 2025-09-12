namespace TraversalDemo.Data
{
    public struct CellAddress
    {
        public int X { get; set; }
        public int Y { get; set; }

        public CellAddress(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}