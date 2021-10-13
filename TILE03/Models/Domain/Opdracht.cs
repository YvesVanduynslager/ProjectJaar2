using Newtonsoft.Json;

namespace TILE03.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Opdracht
    {
        #region Fields

        #endregion

        [JsonProperty]

        #region Properties

        public int Id { get; set; }

        [JsonProperty] public int VolgNr { get; set; }
        [JsonProperty] public Oefening Oefening { get; set; }
        [JsonProperty] public Toegangscode Toegangscode { get; set; }

        [JsonProperty] public bool IsOpgelost { get; set; }
        //[JsonProperty]
        //public int AantalAntwoorden { get; internal set; }

        #endregion

        #region Constructors

        public Opdracht()
        {
            IsOpgelost = false;
        }

        #endregion

        #region Methods

        #endregion
    }
}