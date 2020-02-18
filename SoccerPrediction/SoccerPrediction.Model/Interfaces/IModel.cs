using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    public interface IModel
    {
        bool IsValid { get; }
        ICollection<ValidationResult> Validate();
    }
}
