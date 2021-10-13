namespace TILE03.Models.Domain.BewerkingStrategy
{
    public class Delen : GroepsBewerking
    {
        public double Param { get; set; }

        public Delen()
        {
        }

        public Delen(double param)
        {
            Param = param;
        }

        public override double Transform(double value)
        {
            return (value / Param);
        }

        public override string ToString()
        {
            return "Deel het resultaat door " + Param;
        }
    }
}