using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Interface für alle Modelklassen um eine Validierung zu ermöglichen
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// gibt an ob das Model valide ist
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Validierungs Methode
        /// </summary>
        /// <returns>gibt eine Auflistung von Validierungen zurück</returns>
        ICollection<ValidationResult> Validate();
    }
}
