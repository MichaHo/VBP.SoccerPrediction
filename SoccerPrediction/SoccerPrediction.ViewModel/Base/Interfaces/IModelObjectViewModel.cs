namespace SoccerPrediction.ViewModel
{
    public interface IModelObjectViewModel
    {
        string CreatedBy { get; }
        string CreatedOn { get; }
        string LastUpdateBy { get; }
        string LastUpdateOn { get; }
    }
}
