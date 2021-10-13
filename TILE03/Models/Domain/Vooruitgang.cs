using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TILE03.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Vooruitgang
    {
        [JsonProperty]
        public int Id { get;set; }
        [JsonProperty]
        public string Line { get; set; }
        [JsonProperty]
        public int VolgNr { get; set; }

        public Vooruitgang(){}
        public Vooruitgang(int opdrachtNr, string opdrachtNaam, double antwoord):base()
        {
            VolgNr = opdrachtNr;

            Line = "Oefening nr: " + opdrachtNr + ". Opgave: " + opdrachtNaam + " met antwoord " + antwoord +
                   " is correct";
        }
    }
}
