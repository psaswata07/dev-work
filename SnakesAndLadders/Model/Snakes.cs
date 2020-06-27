using System.Collections.Generic;

namespace SnakesAndLadders.Model
{
    public static class Snakes
    {
        #region Fields

        private static Dictionary<int,int> snakes;

        #endregion

        #region Methods

        public static void PopulateSnakesHeadAndTailPositions(int snakeHeadPosition, int snakeEndPosition)
        {
            if(snakes == null)
            {
                snakes = new Dictionary<int, int>();
            }
            snakes[snakeHeadPosition] = snakeEndPosition;
        }

        public static bool CheckIfCurrentCellPostionIsSnakeHead(int currentCellPosition) => snakes.ContainsKey(currentCellPosition);

        public static int GetSnakeTailForSnakeHead(int currentCellPosition) => snakes[currentCellPosition];

        #endregion

    }
}