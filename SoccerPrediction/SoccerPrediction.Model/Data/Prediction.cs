using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Klasse für den Tip am Spieltag
    /// </summary>
    [Serializable]
    public class Prediction : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        /// <summary>
        /// die Id des Eintrags
        /// </summary>
        [Key]
        public virtual Guid Id { get; set; } = new Guid();

        /// <summary>
        /// Verweis auf den Tipgeber
        /// </summary>
        public virtual Guid PersonId { get; set; }

        /// <summary>
        /// Verweis auf den Spieltag
        /// </summary>
        public virtual Guid GameDayId { get; set; }

        /// <summary>
        /// Verweis auf die Begegnung (die Teams )
        /// </summary>
        public virtual Guid EncounterId { get; set; }

        /// <summary>
        /// Anzahl der Tore für das Heim Team
        /// </summary>
        public virtual int HomeGoal { get; set; }

        /// <summary>
        /// Anzahl der Tore für das Gast Team
        /// </summary>
        public virtual int GuestGoal { get; set; }

        /// <summary>
        /// die Punkte für die Begegnung (zBsp. 2 Punkte für richtiger Tip, 1 Punkt für richtige Tordifferenz)
        /// TODO: diskutieren ob wir dies in einer Settingsklasse erfassen
        /// </summary>
        public virtual int Points { get; set; }

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
