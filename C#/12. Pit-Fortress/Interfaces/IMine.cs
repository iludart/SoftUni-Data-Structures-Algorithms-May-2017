namespace Interfaces
{
    using System;

    using Classes;

    public interface IMine : IComparable<Mine>
    {
        int Id { get; }

        int Delay { get; set; }

        int Damage { get; }

        int XCoordinate { get; }

        Player Player { get; }
    }
}
