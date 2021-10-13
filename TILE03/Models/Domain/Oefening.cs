using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using TILE03.Models.Domain.BewerkingStrategy;

namespace TILE03.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Oefening
    {
        #region Fields

        #endregion

        #region Properties

        [JsonProperty] public int Id { get; set; }
        [JsonProperty] public string Naam { get; set; }
        public GroepsBewerking GroepsBewerking { get; set; }
        [JsonProperty] public byte[] OpgaveFile { get; set; }
        [JsonProperty] public string Feedback { get; set; }
        [JsonProperty] public Antwoord Antwoord { get; set; }

        #endregion

        #region Constructors

        public Oefening()
        {
        }

        #endregion

        #region Methods

        public bool CorrectAntwoord(double value)
        {
            Antwoord.Bewerking = GroepsBewerking;
            return value == Antwoord.Transform();
        }

        #endregion
    }
}