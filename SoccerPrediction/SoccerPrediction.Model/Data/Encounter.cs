using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model.Data
{
    /// <summary>
    /// Klasse für die Begegnung am Spieltag
    /// </summary>
    public class Encounter : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        /// <summary>
        /// die Id des Eintrags
        /// </summary>
        [Key]
        public virtual Guid Id { get; set; } = new Guid();

        /// <summary>
        /// Verweis auf den Spieltag
        /// </summary>
        public virtual int GameDayId { get; set; }

        /// <summary>
        /// Verweis auf das HeimTeam
        /// </summary>
        public virtual int TeamHomeId { get; set; }

        /// <summary>
        /// Verweis auf das GastTeam
        /// </summary>
        public virtual int TeamGuestId { get; set; }

        /// <summary>
        /// Ergebnis des HeimTeams
        /// </summary>
        public virtual int ResultHome { get; set; }

        /// <summary>
        /// Ergebnis des Gastteams
        /// </summary>
        public virtual int ResultGuest { get; set; }

        /// <summary>
        /// Punkte an diesem Spieltag für das Heim Team
        /// </summary>
        public virtual int PointsHome { get; set; }

        /// <summary>
        /// Punkte an diesem Spieltag für das Gast Team
        /// </summary>
        public virtual int PointsGuest { get; set; }

        /// <summary>
        /// Beschreibung des Spieltages (Schalke04 : Eintracht Frankfurt, oder Schalke04 vs. Eintracht Frankfurt)
        /// </summary>
        public virtual string EncounterText { get; set; }

        /// <summary>
        /// Zeigt an ob der Eintrag als gelöscht markiert ist
        /// </summary>
        public virtual bool DeletedFlag { get; set; }

        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag als gelöscht markiert wurde
        /// </summary>
        public virtual DateTime? DeletedTimestamp { get; set; }

        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag zuletzt geändert wurde
        /// </summary>
        public virtual DateTime? LastUpdateTimestamp { get; set; }

        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag angelegt wurde
        /// </summary>
        public virtual DateTime? CreationTimestamp { get; set; } = DateTime.Now;
    }
}
