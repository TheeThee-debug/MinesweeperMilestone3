namespace MinesweeperClassLibrary.Models
{
    public class CellModel
    {
        // Row position of the cell
        public int Row { get; set; }

        // Column position of the cell
        public int Col { get; set; }

        // True if the cell contains a bomb
        public bool IsBomb { get; set; }

        // True if the cell has been revealed
        public bool IsVisited { get; set; }

        // Number of bombs around this cell
        public int LiveNeighbors { get; set; }

        // Default constructor
        public CellModel()
        {
            Row = 0;
            Col = 0;
            IsBomb = false;
            IsVisited = false;
            LiveNeighbors = 0;
        }

        // Parameterized constructor
        public CellModel(int row, int col)
        {
            Row = row;
            Col = col;
            IsBomb = false;
            IsVisited = false;
            LiveNeighbors = 0;
        }
    }
}