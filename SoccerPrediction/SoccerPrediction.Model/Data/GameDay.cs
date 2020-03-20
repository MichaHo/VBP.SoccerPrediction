using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Klasse für einen Spieltag
    /// </summary>
    [Serializable]
    public class GameDay : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        /// <summary>
        /// die Id des Eintrags
        /// </summary>
        [Key]
        public virtual Guid Id { get; set; } = new Guid();

        /// <summary>
        /// Datum des Spieltages
        /// </summary>
        public virtual DateTime Date { get; set; }

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
