using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer
{
    public interface IBoardOperations
    {
        BoardModel BoardModel { get; }

        bool IsValidCell(int row, int col);
        int CalculateLiveNeighbors(int row, int col);
        void RevealCell(int row, int col);
        void FloodFill(int row, int col);
        GameState DetermineGameState();
    }
}