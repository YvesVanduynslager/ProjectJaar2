using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using StackExchange.Redis;
using TILE03.Models.Domain;
using TILE03.Models.Domain.State;

namespace TILE03.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Groep
    {
        #region Fields

        #endregion

        #region Properties
        public GroepState GroepState { get; set; }
        #endregion
        #region JsonPropertiesMappedToDb
        [JsonProperty] public int Id { get; set; }
        [JsonProperty] public UniekPad UniekePad { get; set; }
        [JsonProperty] public ICollection<Leerling> Leerlingen { get; set; }
        [JsonProperty] public string HuidigeState { get; set; }
        [JsonProperty] public int TotaalAantalOefeningen { get; set; }
        [JsonProperty] public int PlaatsInReeks { get; set; }
        [JsonProperty] public ICollection<Vooruitgang> Vooruitgang { get; set; }
        #endregion
        #region JsonPropertiesNotMappedToDb
        [NotMapped][JsonProperty] public Opdracht HuidigeOpdracht { get; set; }
        [NotMapped][JsonProperty] public Actie HuidigeActie { get; set; }
        [NotMapped][JsonProperty]public int AantalVerkeerdeAntwoorden { get; set; }
        [NotMapped][JsonProperty] public double Antwoord { get; set; }

        [NotMapped] [JsonProperty] public string StateDisplayName
        {
            get
            {
                if (HuidigeState == "Aangemeld")
                {
                    return "Aangemeld";
                }
                else if (HuidigeState == "geblokkeerd")
                {
                    return "Geblokkeerd";
                }
                else if (HuidigeState == "Niet Aangemeld")
                {
                    return "Niet Aangemeld";           
                }
                else if (HuidigeState == "ontgrendeld")
                {
                    return "Ontgrendeld";
                }
                else if (HuidigeState == "schatkistgevonden")
                {
                    return "Schat gevonden";
                }
                else if (HuidigeState == "vergrendeld")
                {
                    return "Aan het spelen";
                }
                else
                {
                    return "onbekend";
                }
            }
        }
        #endregion

        #region Constructors

        public Groep()
        {
            Leerlingen = new HashSet<Leerling>();
            Vooruitgang = new List<Vooruitgang>();
            ToState("Niet Aangemeld");
        }

        #endregion

        #region Methods

        public void ToState(string state)
        {
            GroepState = StateFactory.Get(this, state);
            HuidigeState = state;
        }

        public void Start()
        {
            ToState("vergrendeld");
            GroepState.Start();
        }

        public void VolgendeOpdracht()
        {
            GroepState.VolgendeOpdracht();
        }

        public bool GeldigAntwoord(double value)
        {
            return GroepState.GeldigAntwoord(value);
        }

        public void Deblokkeer()
        {
            GroepState.Deblokkeer();
        }

        public void SchatkistGevonden()
        {
            GroepState.SchatkistGevonden();
            
        }

        #endregion
    }
}