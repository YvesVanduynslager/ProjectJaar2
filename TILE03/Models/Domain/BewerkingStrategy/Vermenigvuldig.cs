namespace TILE03.Models.Domain.BewerkingStrategy
{
    public class Vermenigvuldig : GroepsBewerking
    {
        public double Param { get; set; }

        public Vermenigvuldig()
        {
        }

        public Vermenigvuldig(double param)
        {
            Param = param;
        }

        public override double Transform(double value)
        {
            return (value * Param);
        }

        public override string ToString()
        {
            return "Vermenigvuldig het resultaat met " + Param;
        }
    }
}