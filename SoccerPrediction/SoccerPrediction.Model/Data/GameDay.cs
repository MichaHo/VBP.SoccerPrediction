using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Klasse für einen Spieltag
    /// </summary>
    public class GameDay : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual bool DeletedFlag { get; set; }
        public virtual DateTime? DeletedTimestamp { get; set; }
        public virtual DateTime? LastUpdateTimestamp { get; set; }
        public virtual DateTime? CreationTimestamp { get; set; } = DateTime.Now;
    }
}
