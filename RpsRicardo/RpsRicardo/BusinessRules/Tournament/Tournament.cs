using RpsRicardo.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RpsRicardo.BusinessRules.Tournament
{
    public class Tournament
    {
        public PlayerDto rps_tournament_winner(TournamentDto tournament)
        {
            var lastGame = execute_tournament(tournament.Games);

            return new BusinessRules.Game.Game().rps_game_winner(lastGame);
        }
        public GameDto execute_tournament(List<GameDto> games)
        {
            while (games.Count > 1)
            {
                games = execute_brackets(games);
            }
            return games[0];
        }
        public List<GameDto> execute_brackets(List<GameDto> games)
        {
            var gamesList = new List<GameDto>();
            for (int i = 0; i < games.Count(); i += 2)
            {
                gamesList.Add(create_new_game(get_winner(games[i]), get_winner(games[i + 1])));
            }
            return gamesList;
        }
        public PlayerDto get_winner(GameDto game)
        {
            return new BusinessRules.Game.Game().rps_game_winner(game);
        }
        public GameDto create_new_game(PlayerDto player1, PlayerDto player2)
        {
            var newGame = new GameDto();
            newGame.Players = new List<PlayerDto> { player1, player2 };
            return newGame;
        }
    }
}
