namespace TILE03.Models.Domain.BewerkingStrategy
{
    public class Optellen : GroepsBewerking
    {
        public double Param { get; set; }

        public Optellen()
        {
        }

        public Optellen(double param)
        {
            Param = param;
        }

        public override double Transform(double value)
        {
            return (value + Param);
        }

        public override string ToString()
        {
            return "Tel het resultaat op met " + Param;
        }
    }
}