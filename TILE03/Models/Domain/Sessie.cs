using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TILE03.Models.Domain.State;
using TILE03.Models.Repositories;

namespace TILE03.Models.Domain
{
    public class Sessie
    {
        #region Fields

        #endregion

        #region Properties

        public int Id { get; set; }
        public ICollection<Groep> Groepen { get; set; }
        public Klas Klas { get; set; }
        [NotMapped] public Opdracht HuidigeOpdracht { get; set; }

        [NotMapped] public Groep HuidigeGroep { get; set; }

        public string Code { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }

        [NotMapped] public SessieState NonActiefState { get; set; }
        [NotMapped] public SessieState ActiefState { get; set; }
        [NotMapped] public SessieState StartSpelState { get; set; }

        [NotMapped] public SessieState State { get; set; }

        //StateType wordt bijgehouden in de databank om te weten in welke state de sessie zich nu bevindt
        public int StateType { get; set; }
        //voor gebruik in de view
        [NotMapped] public string StateDisplayName
        {
            get
            {
                if (StateType == 0)
                {
                    return "Niet Actief";
                }
                else if (StateType == 1)
                {
                    return "Actief";
                }
                else if (StateType == 2)
                {
                    return "Gestart";
                }
                else
                {
                    return "Onbekend";
                }
            }
        }
        //extra property met logica om te gebruiken in de view zodat dit gemakkelijker 
        //in een javascript functie kan gebruikt worden
        [NotMapped] public bool AlleGroepenAangemeld
        {
            get
            {
                bool value = true;
                foreach (var groep in Groepen)
                {
                    if (groep.HuidigeState == "Niet Aangemeld")
                    {
                        value = false;
                        return value;
                    }
                }
                return value;
            }              
        }

        #endregion

        #region Constructors

        public Sessie()
        {
            //hier worden alle states aangemaakt om makkelijk om te schakelen van state naar state
            //zonder steeds een factory te moeten aanspreken
            NonActiefState = new NonActiefState(this);
            ActiefState = new ActiefState(this);
            StartSpelState = new StartSpelState(this);

            //Omzetten van stateType naar het correct StateObject
            State = StateFactory.GetSessieState(this, StateType);

            Groepen = new HashSet<Groep>();
        }

        #endregion

        #region Methods
        //Bij het veranderen van state moet het stateType ook juist gezet worden voor opslag in de database
        internal void ToState(SessieState state)
        {
            StateType = state.Type;
            State = state;
        }


        public void UitvoerenSessie()
        {
            //Eerste opdracht instellen
            //Update: wordt niet gebruikt
            State.Uitvoeren();
        }

        public void Activeer()
        {
            State.Activeer();
        }

        public void Deactiveer()
        {
            State.Deactiveer();
        }

        public void SpelMagNietStarten()
        {
            State.SpelMagNietStarten();
        }

        public void SpelMagStarten()
        {
            State.SpelMagStarten();
        }

        #endregion
    }
}