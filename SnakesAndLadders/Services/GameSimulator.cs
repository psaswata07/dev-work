using System;
using SnakesAndLadders.Model;

namespace SnakesAndLadders.Services
{
    public static class GameSimulator
    {
        #region Fields

        static bool gameOver = false;

        static int numberOfPlayers;

        #endregion
        
        #region Methods

        public static void Play(Player[] players, Random dice)
        {
            GameSimulator.numberOfPlayers = players.Length;

            while(!GameSimulator.gameOver)
            {
                int currentPlayerIterator = 0;
                while(currentPlayerIterator < numberOfPlayers)
                {
                    var currentDicePosition = dice.Next(1,7);
                    var previousCellPosition = players[currentPlayerIterator].GetCurrentCellPosition();

                    if(players[currentPlayerIterator].GetCurrentCellPosition() == 0 && currentDicePosition == 6)
                    {
                        players[currentPlayerIterator].IncrementCurrentCellPosition(currentDicePosition);

                        if(GameSimulator.CheckGameStatus(players[currentPlayerIterator]))
                        {
                            DisplayWinningMessage(players[currentPlayerIterator]);
                            break;
                        }

                        GameSimulator.CheckIfCurrentCellPositionIsSnakeHead(players[currentPlayerIterator]);
                        GameSimulator.CheckIfCurrentCellPositionIsLadderStart(players[currentPlayerIterator]);

                        if(GameSimulator.CheckGameStatus(players[currentPlayerIterator]))
                        {
                            DisplayWinningMessage(players[currentPlayerIterator]);
                            break;
                        }
                                                
                        DisplayMoveMessage(players[currentPlayerIterator].GetName(), currentDicePosition, previousCellPosition, players[currentPlayerIterator].GetCurrentCellPosition());
                    }

                    else if(players[currentPlayerIterator].GetCurrentCellPosition() != 0)
                    {
                        if(previousCellPosition + currentDicePosition >100)
                        {
                            continue;
                        }                       

                        players[currentPlayerIterator].IncrementCurrentCellPosition(currentDicePosition);

                        if(GameSimulator.CheckGameStatus(players[currentPlayerIterator]))
                        {
                            DisplayWinningMessage(players[currentPlayerIterator]);
                            break;
                        }

                        GameSimulator.CheckIfCurrentCellPositionIsSnakeHead(players[currentPlayerIterator]);
                        GameSimulator.CheckIfCurrentCellPositionIsLadderStart(players[currentPlayerIterator]);

                        if(GameSimulator.CheckGameStatus(players[currentPlayerIterator]))
                        {
                            DisplayWinningMessage(players[currentPlayerIterator]);
                            break;
                        }

                        DisplayMoveMessage(players[currentPlayerIterator].GetName(), currentDicePosition, previousCellPosition, players[currentPlayerIterator].GetCurrentCellPosition());
                    }
                    
                    GameSimulator.IncrementCurrentPlayerIterator(ref currentPlayerIterator);
                }
            }
        }

        private static void IncrementCurrentPlayerIterator(ref int currentPlayerIterator)
        {
            if(currentPlayerIterator == GameSimulator.numberOfPlayers - 1)
                currentPlayerIterator = 0;
            else
                currentPlayerIterator++;

        }

        private static bool CheckGameStatus(Player player)
        {
            GameSimulator.UpdateGameStatus(player);
            return GameSimulator.gameOver;
        }

        private static void UpdateGameStatus(Player player)
        {
            if(player.GetCurrentCellPosition() == 100)
            {
                gameOver = true;
            }
        }

        private static void CheckIfCurrentCellPositionIsLadderStart(Player player)
        {
            if(Ladders.CheckIfCurrentCellPositionIsLadderStart(player.GetCurrentCellPosition()))
            {
                player.SetCurrentCellPosition(Ladders.GetLadderEndForLadderStart(player.GetCurrentCellPosition()));
            }
        }

        private static void CheckIfCurrentCellPositionIsSnakeHead(Player player)
        {
            if(Snakes.CheckIfCurrentCellPostionIsSnakeHead(player.GetCurrentCellPosition()))
            {
                player.SetCurrentCellPosition(Snakes.GetSnakeTailForSnakeHead(player.GetCurrentCellPosition()));
            }            
        }

        private static void DisplayWinningMessage(Player player)
        {
            Console.WriteLine(player.GetName() + " wins the game ");
        }

        private static void DisplayMoveMessage(string playerName, int currentDicePosition, int previousCellPosition, int currentCellPosition)
        {
            Console.WriteLine(playerName + " rolled a {0} and moved from {1} to {2}", currentDicePosition, previousCellPosition, currentCellPosition);
        }

        #endregion
    }
}