using System;
using System.Collections.Generic;

public class Piece
{
    public class Piece : IMovable, IDrawable
    {
        public Cell Position { get; set; }
        public Player Player { get; set; }

        public  void Move();
        public List<Move> ValidMoves();

        public void Display()
        {
            // Implement logic to display the piece
            // ...
        }
    }
}
