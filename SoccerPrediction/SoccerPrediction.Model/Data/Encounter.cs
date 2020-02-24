using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoccerPrediction.Model.Data
{
    /// <summary>
    /// Klasse für die Begegnung am Spieltag
    /// </summary>
    public class Encounter : ModelBase, ILogicalDelete, ILogicalTimestamp
    {

        [Key]
        public virtual Guid Id { get; set; } = new Guid();
        public virtual int GameDayId { get; set; }
        public virtual int TeamHomeId { get; set; }
        public virtual int TeamGuestId { get; set; }
        public virtual int ResultHome { get; set; }
        public virtual int ResultGuest { get; set; }
        public virtual int PointsHome { get; set; }
        public virtual int PointsGuest { get; set; }
        public virtual string EncounterText { get; set; }

        public virtual bool DeletedFlag { get; set; }
        public virtual DateTime? DeletedTimestamp { get; set; }
        public virtual DateTime? LastUpdateTimestamp { get; set; }
        public virtual DateTime? CreationTimestamp { get; set; } = DateTime.Now;
    }
}
