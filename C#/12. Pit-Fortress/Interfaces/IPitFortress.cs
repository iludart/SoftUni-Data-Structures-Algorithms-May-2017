namespace Interfaces
{
    using System.Collections.Generic;

    using Classes;

    public interface IPitFortress
    {
        int PlayersCount { get; }

        int MinionsCount { get; }

        int MinesCount { get; }

        void AddPlayer(string name, int mineRadius);

        void AddMinion(int xCoordinate);

        void SetMine(string playerName, int xCoordinate, int delay, int damage);

        IEnumerable<Minion> ReportMinions();

        IEnumerable<Player> Top3PlayersByScore();

        IEnumerable<Player> Min3PlayersByScore();

        IEnumerable<Mine> GetMines();

        void PlayTurn();
    }
}
