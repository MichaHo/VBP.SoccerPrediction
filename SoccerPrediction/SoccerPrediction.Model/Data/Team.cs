using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoccerPrediction.Model.Data
{
    /// <summary>
    /// Klasse für das Verein Team
    /// </summary>
    public class Team : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        [Key]
        public virtual Guid Id { get; set; } = new Guid();
        public virtual string Name { get; set; }
        public virtual int RankPoints { get; set; }

        public virtual bool DeletedFlag { get; set; }
        public virtual DateTime? DeletedTimestamp { get; set; }
        public virtual DateTime? LastUpdateTimestamp { get; set; }
        public virtual DateTime? CreationTimestamp { get; set; } = DateTime.Now;
    }
}
