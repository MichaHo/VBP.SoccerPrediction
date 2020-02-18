using System;

namespace SoccerPrediction.Model
{
    public interface ILogicalDelete
    {
        bool DeletedFlag { get; set; }
        DateTime? DeletedTimestamp { get; set; }
    }
}
