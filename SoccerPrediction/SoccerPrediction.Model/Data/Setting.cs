using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    public class Setting
    {

        [Key]
        public virtual int SettingId { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Key { get; set; }

        [Required]
        public virtual string Value { get; set; }

        public virtual int SettingInfoId { get; set; }

        [Required]
        public virtual SettingInformation SettingInfo { get; set; }

        public override string ToString()
        {
            return $"SettingObject: ID: {SettingId} Key: {Key.ToString()}, Value: {Value.ToString()}";
        }

    }
}
