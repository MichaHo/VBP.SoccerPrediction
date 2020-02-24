using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Klasse für den Tipgeber
    /// </summary>
    public class Person : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int TotalPoints { get; set; }
        public virtual int GameDayPoints { get; set; }
        public virtual AccessData Credentials { get; set; }
        public virtual bool DeletedFlag { get; set; }
        public virtual DateTime? DeletedTimestamp { get; set; }
        public virtual DateTime? LastUpdateTimestamp { get; set; }
        public virtual DateTime? CreationTimestamp { get; set; } = DateTime.Now;
    }
}
