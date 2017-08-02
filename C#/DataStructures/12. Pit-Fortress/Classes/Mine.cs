namespace Classes
{
    using Interfaces;

    public class Mine : IMine
    {
        public Mine(int id, int delay, int damage, int x, Player player)
        {
            this.Id = id;
            this.Delay = delay;
            this.Damage = damage;
            this.XCoordinate = x;
            this.Player = player;
        }

        public int CompareTo(Mine other)
        {
            int cmp = this.Delay.CompareTo(other.Delay);
            if (cmp == 0)
            {
                cmp = this.Id.CompareTo(other.Id);
            }

            return cmp;
        }

        public int Id { get; private set; }

        public int Delay { get; set; }

        public int Damage { get; private set; }

        public int XCoordinate { get; private set; }

        public Player Player { get; private set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Player: {1}, Radius: {2}, X: {3}", this.Id, this.Player.Name, this.Player.Radius, this.XCoordinate);
        }
    }
}
