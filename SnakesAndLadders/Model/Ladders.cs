using System.Collections.Generic;

namespace SnakesAndLadders.Model
{
    public static class Ladders
    {
        #region Fields

        private static Dictionary<int,int> ladders;

        #endregion

        #region Methods

        public static void PopulateLaddersStartAndEndPostions(int ladderStartPosition, int ladderEndPosition)
        {
            if(ladders == null)
            {
                ladders = new Dictionary<int, int>();
            }

            ladders[ladderStartPosition] = ladderEndPosition;
        }

        public static bool CheckIfCurrentCellPositionIsLadderStart(int currentCellPosition) => ladders.ContainsKey(currentCellPosition);

        public static int GetLadderEndForLadderStart(int currentCellPosition) => ladders[currentCellPosition];

        #endregion
    }
}