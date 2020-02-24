using System;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Interface für alle Model Klassen um das Anlegen und daas Änder zu dokumentieren
    /// </summary>
    public interface ILogicalTimestamp
    {
        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag zuletzt bearbeitet wurde
        /// </summary>
        DateTime? LastUpdateTimestamp { get; set; }

        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag angelegt wurde
        /// </summary>
        DateTime? CreationTimestamp { get; set; }
    }
}
