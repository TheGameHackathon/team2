using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace thegame.Domain.Game
{
    public class SmartGame : GameBase
    {
        private Cell leftUpCell;
        private int Score { get; set; }

        public bool IsFinished
        {
            get => isFinished();
        }
        private bool isFinished()=>
            gameField.Cells.All(x => x.All(y => y.Color == leftUpCell.Color));

        public SmartGame()
        {

        }
        public SmartGame(Guid id, GameField gameField) : base(id, gameField)
        {
            leftUpCell = gameField.Cells[0][0];
        }

        public override void MakeMove(Vector move)
        {
            leftUpCell = gameField.Cells[0][0];
            if (leftUpCell.Color == gameField.Cells[move.Y][move.X].Color)
                return;
            var chosenColor = gameField.Cells[move.Y][move.X].Color;
            var startColor = leftUpCell.Color;
            BFS(startColor,chosenColor);
        }

        private void BFS(Color startColor, Color chosenColor)
        {
            int doneCells=0;
            var queue = new Queue<Cell>();
            var visited = new HashSet<Cell>();
            queue.Enqueue(leftUpCell);

            while (queue.Count!=0)
            {
                var toOpen = queue.Dequeue();
                visited.Add(toOpen);
                foreach (var neighbor in GetNeighbors(toOpen,visited))
                {
                    if (neighbor.Color == startColor)
                        queue.Enqueue(neighbor);
                }
                gameField.Cells[toOpen.Coordinates.Y][toOpen.Coordinates.X].Color = chosenColor;
            }
        }

        private List<Cell> GetNeighbors(Cell toopen, HashSet<Cell> visited)
        {
            var directions = new[] {new Point(0, 1), new Point(0,-1), new Point(1,0), new Point(-1,0)};
            var result = new List<Cell>();
            foreach (var direction in directions)
            {
                var newPos = new Point(toopen.Coordinates.X + direction.X, (toopen.Coordinates.Y + direction.Y));
                if (!(newPos.X >= 0 && newPos.Y >= 0 && newPos.X < gameField.Width && newPos.Y < gameField.Height))
                    continue;
                if (visited.Contains(gameField.Cells[newPos.Y][newPos.X]))
                    continue;
                
                result.Add(gameField.Cells[newPos.Y][newPos.X]);
            }

            return result;
        }
    }
}
