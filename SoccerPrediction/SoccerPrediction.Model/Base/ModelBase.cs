using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Basisklasse für alle Model Klassen mit Validierung
    /// </summary>
    public abstract class ModelBase : IModel
    {
        /// <summary>
        /// der Username (Windows User) der diesen Eintrag erstellt hat
        /// </summary>
        public virtual string CreatedBy { get; set; } = Environment.UserName;

        /// <summary>
        /// Windows PC Name auf dem dieser Eintrag erstellt wurde
        /// </summary>
        public virtual string CreatedOn { get; set; } = Environment.MachineName;

        /// <summary>
        /// der User (Windows) von dem dieser Eintrag zuletzt bearbeitet wurde
        /// </summary>
        public virtual string LastUpdatedBy { get; set; } = Environment.UserName;

        /// <summary>
        /// PC (Windows) Name auf dem dieser Eintrag zuletzt bearbeitet wurde
        /// </summary>
        public virtual string LastUpdatedOn { get; set; } = Environment.MachineName;

        /// <summary>
        /// gibt an ob das Model valide ist
        /// </summary>
        public bool IsValid => Validate() == null || Validate().Count == 0;

        /// <summary>
        /// Validierungs Methode
        /// </summary>
        /// <returns>gibt eine Auflistung von Validierungen zurück</returns>
        public ICollection<ValidationResult> Validate()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);
            Validator.TryValidateObject(this, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
