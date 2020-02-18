using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    public class SettingInformation : ModelBase, ILogicalTimestamp
    {

        [Key]
        public virtual int SettingInformationId { get; set; }

        [Required]
        public virtual string DefaultValue { get; set; }

        [Required]
        public virtual SettingCategoryEnum Category { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        [MinLength(3)]
        public virtual string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(3)]
        public virtual string Description { get; set; }

        public virtual string Unit { get; set; }

        public virtual string MinValue { get; set; }

        public virtual string MaxValue { get; set; }

        [Required]
        public virtual bool IsAutomated { get; set; }

        [Required]
        public virtual DataTypeEnum DataType { get; set; }

        public virtual ICollection<Setting> Settings { get; set; }

        public virtual DateTime? LastUpdateTimestamp { get; set; }
        public virtual DateTime? CreationTimestamp { get; set; }
    }

    public enum DataTypeEnum
    {
        TypeString = 0,
        TypeBoolean = 1,
        TypeInteger = 2,
        TypeDouble = 3,
        TypeStringFromList = 4
    }
    public enum SettingCategoryEnum
    {
        SettCatUi = 0,
        SettCatFocus = 3,
        SettCatDefaultValues = 4,
        SettCatPrinter = 5,
        SettCatSystem = 6
    }
}
