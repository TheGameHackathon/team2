using System;


namespace thegame.Domain.Game
{
    public class StupidGame : GameBase
    {
        public StupidGame(Guid id, GameField gameField) : base(id, gameField)
        {
        }

        public override void MakeMove(Vector move)
        {
            gameField.Cells[move.Y][move.X] = GetCellWithRandomColor(move);
        }

        private Cell GetCellWithRandomColor(Vector coordinates)
        {
            return new Cell(coordinates, Color.Red);
        }
    }
}
