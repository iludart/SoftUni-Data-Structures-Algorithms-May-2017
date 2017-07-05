namespace Interfaces
{
    using System;

    using Classes;

    public interface IPlayer : IComparable<Player>
    {
        string Name { get; }

        int Radius { get; }

        int Score { get; set; }
    }
}
