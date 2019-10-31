using System;
using System.Linq;
using RpsRicardo.Entities;

namespace RpsRicardo.BusinessRules.Game
{
    public class Game
    {
        public PlayerDto rps_game_winner(GameDto game)
        {
            if (game.Players.Count() != 2)
                throw new Exception("WrongNumberOfPlayersError");

            foreach (var player in game.Players)
            {
                if (!verify_valid_move(player))
                    throw new Exception("NoSuchStrategyError");
            }

            return verify_winner(game);
        }

        private bool verify_valid_move(PlayerDto player)
        {
            var move = Char.ToUpper(player.Move);

            if (move.Equals('R') || move.Equals('P') || move.Equals('S'))
                return true;

            return false;
        }

        private PlayerDto verify_winner(GameDto game)
        {
            char moveForSecondPlayerWins = 'P';
            switch (char.ToUpper(game.Players[0].Move))
            {
                case 'P':
                    moveForSecondPlayerWins = 'S';
                    break;
                case 'S':
                    moveForSecondPlayerWins = 'R';
                    break;
            }
            return verify_who_wins(game, moveForSecondPlayerWins);
        }


        private PlayerDto verify_who_wins(GameDto game, char moveForSecondPlayerWins)
        {
            var winner = game.Players[0];

            if (char.ToUpper(game.Players[1].Move).Equals(moveForSecondPlayerWins))
                winner = game.Players[1];

            return winner;
        }
    }
}
