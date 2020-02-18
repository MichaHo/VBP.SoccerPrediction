using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoccerPrediction.Model
{
    public class Protocol : ModelBase, ILogicalTimestamp
    {
        [Key]
        public int ProtocolId { get; set; }

        [Required]
        public string ProtocolText { get; set; }

        public DateTime? LastUpdateTimestamp { get; set; }
        public DateTime? CreationTimestamp { get; set; }

        public Protocol()
        { }
        public Protocol(string protocolText, string createdByUser)
        {
            ProtocolText = protocolText;
            CreatedBy = createdByUser;
        }

        #region Linked To

        //TODO: LinkedTo hinzufügen

        #endregion

    }
}
