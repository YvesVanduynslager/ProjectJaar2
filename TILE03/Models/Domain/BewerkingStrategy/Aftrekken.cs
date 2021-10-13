namespace TILE03.Models.Domain.BewerkingStrategy
{
    public class Aftrekken : GroepsBewerking
    {
        public double Param { get; set; }

        public Aftrekken()
        {
        }

        public Aftrekken(double param)
        {
            Param = param;
        }

        public override double Transform(double value)
        {
            return (value - Param);
        }

        public override string ToString()
        {
            return "Trek het resultaat af met " + Param;
        }
    }
}