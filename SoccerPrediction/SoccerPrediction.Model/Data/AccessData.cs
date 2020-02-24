using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    public class AccessData : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string UserName { get; set; }
        public virtual string EncryptedPassword { get; set; }
        public virtual AccessDataTypeEnum UserType { get; set; }
        public virtual DateTime? LastUpdateTimestamp { get; set; }
        public virtual DateTime? CreationTimestamp { get; set; } = DateTime.Now;
        public virtual bool DeletedFlag { get; set; }
        public virtual DateTime? DeletedTimestamp { get; set; }
    }

    public enum AccessDataTypeEnum
    {
        None = 0,
        User = 1,
        Admin = 2,
        AdminUser = 3
    }
}
