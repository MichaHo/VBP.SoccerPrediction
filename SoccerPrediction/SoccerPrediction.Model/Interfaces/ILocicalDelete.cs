using System;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Interface für Klassen um das Löschen zu dokumentieren
    /// </summary>
    public interface ILogicalDelete
    {
        /// <summary>
        /// zeigt an, ob der Eintrag als gelöscht markiert wurde
        /// </summary>
        bool DeletedFlag { get; set; }

        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag als gelöscht markiert wurde
        /// </summary>
        DateTime? DeletedTimestamp { get; set; }
    }
}
