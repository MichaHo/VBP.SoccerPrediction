using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    public abstract class ModelBase : IModel
    {
        public virtual string CreatedBy { get; set; } = Environment.UserName;
        public virtual string CreatedOn { get; set; } = Environment.MachineName;
        public virtual string LastUpdatedBy { get; set; } = Environment.UserName;
        public virtual string LastUpdatedOn { get; set; } = Environment.MachineName;

        public bool IsValid => Validate() == null || Validate().Count == 0;

        public ICollection<ValidationResult> Validate()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);
            Validator.TryValidateObject(this, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
