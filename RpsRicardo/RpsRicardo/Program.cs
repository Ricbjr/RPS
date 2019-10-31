using System;
using RpsRicardo.Entities;
using RpsRicardo.BusinessRules.Tournament;
using System.Collections.Generic;
using System.Linq;
namespace RpsRicardo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert the game like this:");
            Console.WriteLine("[[[[\"Armando\",\"P\"],[\"Dave\",\"S\"]],[[\"Richard\",\"R\"],[\"Michael\",\"S\"]],]," +
                "[[[\"Allen\",\"S\"],[\"Omer\",\"P\"]],[[\"David E.\",\"R\"],[\"Richard X.\",\"P\"]]]]");
            Console.WriteLine("Input must have no spaces and must be on the same line.\n");
            string input = Console.ReadLine();
            var gamesList = new List<GameDto>();

            gamesList.AddRange(extract_games(input));

            var tournament = new TournamentDto
            {
                Games = gamesList
            };
            try
            {
                var winner = new Tournament().rps_tournament_winner(tournament);
                Console.WriteLine("[\"" + winner.Name + "\",\"" + winner.Move + "\"]");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }

        static List<GameDto> extract_games(string input)
        {
            var players = extract_players(input);
            var games = new List<GameDto>();

            for (int i = 0; i < players.Count; i += 2)
                games.Add(new Tournament().create_new_game(players[i], players[i + 1]));

            return games;
        }

        static List<PlayerDto> extract_players(string input)
        {
            var players = new List<PlayerDto>();

            var stringList = prepare_strings(input);
            
            for(int i = 0; i<stringList.Count+1;i+=2)
            {
                var player = new PlayerDto
                {
                    Name = stringList[i].Replace("\"", ""),
                    Move = stringList[i+1].Replace("\"", "")[0]
                };
                players.Add(player);
            }
            return players;
        }

        static List<string> prepare_strings(string input)
        {
            input = input.Replace("[", "");
            input = input.Replace("]", "");
            List<string> stringList = input.Split(',').ToList();

            return stringList = stringList.Where(x => !x.Equals("")).ToList();
        }

    }
}
