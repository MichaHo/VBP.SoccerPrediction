using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerPrediction.Model
{
    public interface ILogicalTimestamp
    {
        DateTime? LastUpdateTimestamp { get; set; }
        DateTime? CreationTimestamp { get; set; }
    }
}
