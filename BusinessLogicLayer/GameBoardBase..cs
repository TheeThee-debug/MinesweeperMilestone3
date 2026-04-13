using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer
{
    public abstract class GameBoardBase
    {
        // Encapsulated board model
        protected BoardModel boardModel;

        // Public access to board data
        public BoardModel BoardModel
        {
            get { return boardModel; }
        }

        protected GameBoardBase(int size)
        {
            boardModel = new BoardModel(size);
        }

        // Shared validation logic for all board types
        public virtual bool IsValidCell(int row, int col)
        {
            return row >= 0 && row < boardModel.Size &&
                   col >= 0 && col < boardModel.Size;
        }

        // Force child classes to define reveal behavior
        public abstract void RevealCell(int row, int col);
    }
}