using RpsRicardo.Entities;

namespace RpsRicardo.BusinessRules.Game
{
    public interface IGame
    {
        PlayerDto rps_game_winner(GameDto game);
    }
}
