using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoccerPrediction.Model.Data
{
    /// <summary>
    /// Klasse für den Tip am Spieltag
    /// </summary>
    public class Prediction : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        [Key]
        public virtual Guid Id { get; set; } = new Guid();
        public virtual int PersonId { get; set; }
        public virtual int GameDayId { get; set; }
        public virtual int EncounterId { get; set; }
        public virtual int HomeGoal { get; set; }
        public virtual int GuestGoal { get; set; }
        public virtual int Points { get; set; }

        public virtual bool DeletedFlag { get; set; }
        public virtual DateTime? DeletedTimestamp { get; set; }
        public virtual DateTime? LastUpdateTimestamp { get; set; }
        public virtual DateTime? CreationTimestamp { get; set; } = DateTime.Now;
    }
}
