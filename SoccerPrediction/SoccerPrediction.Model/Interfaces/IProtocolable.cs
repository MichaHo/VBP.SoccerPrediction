using System.Collections.Generic;

namespace SoccerPrediction.Model
{
    public interface IProtocolable
    {
        ICollection<Protocol> Protocols { get; set; }
    }
}
