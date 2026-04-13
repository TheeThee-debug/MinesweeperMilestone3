using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer
{
    public class Board : GameBoardBase, IBoardOperations
    {
        // Constructor
        public Board(int size) : base(size)
        {
            InitializeBoard();
            PlaceBombs();
            SetupLiveNeighbors();
        }

        // Create all cell objects
        private void InitializeBoard()
        {
            for (int row = 0; row < boardModel.Size; row++)
            {
                for (int col = 0; col < boardModel.Size; col++)
                {
                    boardModel.Cells[row, col] = new CellModel(row, col);
                }
            }
        }

        // Place bombs in fixed locations for predictable testing
        private void PlaceBombs()
        {
            boardModel.Cells[1, 1].IsBomb = true;
            boardModel.Cells[2, 3].IsBomb = true;
            boardModel.Cells[4, 4].IsBomb = true;
            boardModel.Cells[6, 2].IsBomb = true;
            boardModel.Cells[7, 7].IsBomb = true;
        }

        // Calculate neighbor counts for all non-bomb cells
        private void SetupLiveNeighbors()
        {
            for (int row = 0; row < boardModel.Size; row++)
            {
                for (int col = 0; col < boardModel.Size; col++)
                {
                    if (!boardModel.Cells[row, col].IsBomb)
                    {
                        boardModel.Cells[row, col].LiveNeighbors = CalculateLiveNeighbors(row, col);
                    }
                    else
                    {
                        // Optional visual marker for bomb cells during testing
                        boardModel.Cells[row, col].LiveNeighbors = 9;
                    }
                }
            }
        }

        // Count bombs around a specific cell
        public int CalculateLiveNeighbors(int row, int col)
        {
            int liveNeighbors = 0;

            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    if (IsValidCell(r, c) && !(r == row && c == col))
                    {
                        if (boardModel.Cells[r, c].IsBomb)
                        {
                            liveNeighbors++;
                        }
                    }
                }
            }

            return liveNeighbors;
        }

        // Override from abstract base class
        public override void RevealCell(int row, int col)
        {
            // Stop if the selected cell is outside the board
            if (!IsValidCell(row, col))
            {
                return;
            }

            CellModel selectedCell = boardModel.Cells[row, col];

            // Stop if the cell has already been revealed
            if (selectedCell.IsVisited)
            {
                return;
            }

            // If the selected cell is a bomb, reveal only that bomb
            if (selectedCell.IsBomb)
            {
                selectedCell.IsVisited = true;
                boardModel.CurrentGameState = GameState.Lost;
                return;
            }

            // Otherwise, use recursive flood fill
            FloodFill(row, col);

            // Re-check the game state after revealing
            DetermineGameState();
        }

        // Recursive flood fill method
        public void FloodFill(int row, int col)
        {
            // Stop if the cell is outside the board
            if (!IsValidCell(row, col))
            {
                return;
            }

            CellModel currentCell = boardModel.Cells[row, col];

            // Stop if already visited
            if (currentCell.IsVisited)
            {
                return;
            }

            // Stop if this cell is a bomb
            if (currentCell.IsBomb)
            {
                return;
            }

            // Reveal the current cell
            currentCell.IsVisited = true;

            // If the cell has neighboring bombs, stop recursion here
            if (currentCell.LiveNeighbors > 0)
            {
                return;
            }

            // Recursive calls to all 8 neighbors
            FloodFill(row - 1, col);     // Up
            FloodFill(row + 1, col);     // Down
            FloodFill(row, col - 1);     // Left
            FloodFill(row, col + 1);     // Right
            FloodFill(row - 1, col - 1); // Top-left
            FloodFill(row - 1, col + 1); // Top-right
            FloodFill(row + 1, col - 1); // Bottom-left
            FloodFill(row + 1, col + 1); // Bottom-right
        }

        // Determine whether the game is won, lost, or still playing
        public GameState DetermineGameState()
        {
            // If any bomb is visited, the game is lost
            for (int row = 0; row < boardModel.Size; row++)
            {
                for (int col = 0; col < boardModel.Size; col++)
                {
                    CellModel currentCell = boardModel.Cells[row, col];

                    if (currentCell.IsBomb && currentCell.IsVisited)
                    {
                        boardModel.CurrentGameState = GameState.Lost;
                        return GameState.Lost;
                    }
                }
            }

            // Check if all safe cells have been visited
            for (int row = 0; row < boardModel.Size; row++)
            {
                for (int col = 0; col < boardModel.Size; col++)
                {
                    CellModel currentCell = boardModel.Cells[row, col];

                    if (!currentCell.IsBomb && !currentCell.IsVisited)
                    {
                        boardModel.CurrentGameState = GameState.StillPlaying;
                        return GameState.StillPlaying;
                    }
                }
            }

            boardModel.CurrentGameState = GameState.Won;
            return GameState.Won;
        }
    }
}