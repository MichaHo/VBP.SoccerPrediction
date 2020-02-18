using System;

namespace SoccerPrediction.ViewModel
{
    public interface ITimestampModelViewModel
    {
        DateTime? LastUpdateModelTimestamp { get; }
        DateTime? CreationModelTimestamp { get; }
    }
}
