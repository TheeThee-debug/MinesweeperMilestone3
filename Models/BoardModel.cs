namespace MinesweeperClassLibrary.Models
{
    public class BoardModel
    {
        // Size of the board (example: 8 means 8x8)
        public int Size { get; set; }

        // 2D array of cells
        public CellModel[,] Cells { get; set; }

        // Current state of the game
        public GameState CurrentGameState { get; set; }

        public BoardModel(int size)
        {
            Size = size;
            Cells = new CellModel[size, size];
            CurrentGameState = GameState.StillPlaying;
        }
    }
}