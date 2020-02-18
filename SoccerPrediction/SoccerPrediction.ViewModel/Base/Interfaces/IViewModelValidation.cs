using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.ViewModel
{
    public interface IViewModelValidation
    {
        bool IsValid { get; }
        List<ValidationResult> ValidationErrors();
    }
}
