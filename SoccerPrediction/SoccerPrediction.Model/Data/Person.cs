using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Klasse für den Tipgeber
    /// </summary>
    [Serializable]
    public class Person : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        /// <summary>
        /// die Id des Eintrags
        /// </summary>
        [Key]
        public virtual Guid Id { get; set; } = new Guid();

        /// <summary>
        /// Vorname des Tipgebers
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Nachname des Tipgebers
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gesamtanzahl der bisher erreichten Tip Punkte
        /// </summary>
        public virtual int TotalPoints { get; set; }

        /// <summary>
        /// erreichte Punkte am Spieltag
        /// </summary>
        public virtual int GameDayPoints { get; set; }

        /// <summary>
        /// Anmeldedaten des Tipgebers
        /// </summary>
        public virtual AccessData Credentials { get; set; }
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
