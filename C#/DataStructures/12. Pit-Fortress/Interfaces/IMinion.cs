namespace Interfaces
{
    using System;

    using Classes;

    public interface IMinion : IComparable<Minion>
    {
        int Id { get; }

        int XCoordinate { get; }

        int Health { get; set; }
    }
}
