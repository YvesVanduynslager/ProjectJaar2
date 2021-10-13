using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TILE03.Models.Domain.BewerkingStrategy;

namespace TILE03.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Antwoord
    {
        #region Fields

        #endregion

        #region Properties

        public int Id { get; set; }
        [JsonProperty] public double Value { get; set; }
        [NotMapped] public GroepsBewerking Bewerking { get; set; }

        #endregion

        #region Constructors

        public Antwoord()
        {
        }

        public Antwoord(double value)
        {
            Value = value;
        }

        #endregion

        #region Methods

        public double Transform()
        {
            return Bewerking.Transform(Value);
        }

        #endregion
    }
}