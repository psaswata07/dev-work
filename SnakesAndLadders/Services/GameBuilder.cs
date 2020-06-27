using SnakesAndLadders.Model;
using System;

namespace SnakesAndLadders.Services
{
    public static class GameBuilder
    {
        #region Method

        public static void Build()
        {
            GameBuilder.ParseSnakesFromInput();
            GameBuilder.ParseLaddersFromInput();

            var players = GameBuilder.ParsePlayersFromInput();
            var dice = GameBuilder.CreateDice();

            GameSimulator.Play(players, dice);
        }

        private static void ParseSnakesFromInput()
        {
            int numberOfSnakes = int.Parse(Console.ReadLine());

            for(int i = 0; i<numberOfSnakes; i++)
            {
                var snakeHeadAndTailPositionAsString = Console.ReadLine();
                var snakeHeadAndTailPositionAsStringArray = snakeHeadAndTailPositionAsString.Split(' ');
                Snakes.PopulateSnakesHeadAndTailPositions(int.Parse(snakeHeadAndTailPositionAsStringArray[0]), int.Parse(snakeHeadAndTailPositionAsStringArray[1]));                
            }
        }

        private static void ParseLaddersFromInput()
        {
            int numberOfLadders = int.Parse(Console.ReadLine());

            for(int i = 0; i<numberOfLadders; i++)
            {
                var laddersStartAndEndPositionAsString = Console.ReadLine();
                var laddersStartAndEndPositionAsStringArray = laddersStartAndEndPositionAsString.Split(' ');
                Ladders.PopulateLaddersStartAndEndPostions(int.Parse(laddersStartAndEndPositionAsStringArray[0]), int.Parse(laddersStartAndEndPositionAsStringArray[1]));                
            }
        }

        private static Player[] ParsePlayersFromInput()
        {
            int numberOfPlayers = int.Parse(Console.ReadLine());
            Player[] players = new Player[numberOfPlayers];

            for(int i = 0; i<numberOfPlayers; i++)
            {
                var name = Console.ReadLine();
                players[i] = new Player(name);
            }

            return players;
        }

        private static Random CreateDice()
        {
            return new Random();
        }

        #endregion;

    }
}