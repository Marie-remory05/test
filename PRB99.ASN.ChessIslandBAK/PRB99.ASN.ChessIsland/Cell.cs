using System;

public class Cell
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CellState State { get; set; }
        public Piece Piece { get; set; }

        public bool IsEmpty()
        {
            return Piece == null;
        }


        public void Display()
        {
            // Implement logic to display the cell on the board
            // ...
        }
    }

    
    public enum CellState
    {
        Empty,
        Occupied,
        Flag
    }
}
