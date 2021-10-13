namespace TILE03.Models.Domain.BewerkingStrategy
{
    public abstract class GroepsBewerking
    {
        public int Id { get; set; }

        public abstract double Transform(double value);
        public abstract override string ToString();
    }
}