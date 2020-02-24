using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerPrediction.Model
{
    /// <summary>
    /// Klasse für Login Daten einer Person
    /// </summary>
    public class AccessData : ModelBase, ILogicalDelete, ILogicalTimestamp
    {
        /// <summary>
        /// die Id des Eintrags
        /// </summary>
        [Key]
        public virtual Guid Id { get; set; } = new Guid();

        /// <summary>
        /// der Username (muss angegeben werden und mindestens 3 Zeichen sind erforderlich)
        /// </summary>
        [Required]
        [MinLength(3)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// das verschlüsselte Kennwort
        /// TODO: diskutieren ob wir hier einen SecureString verwenden oder nur hashen
        /// </summary>
        [Required]
        [MinLength(8)]
        public virtual string EncryptedPassword { get; set; }

        /// <summary>
        /// der Typ des Users
        /// TODO: diskutieren, ob hier ein bool Wert reicht
        /// </summary>
        public virtual AccessDataTypeEnum UserType { get; set; }

        /// <summary>
        /// Zeigt an ob der Eintrag als gelöscht markiert ist
        /// </summary>
        public virtual bool DeletedFlag { get; set; }

        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag als gelöscht markiert wurde
        /// </summary>
        public virtual DateTime? DeletedTimestamp { get; set; }

        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag zuletzt geändert wurde
        /// </summary>
        public virtual DateTime? LastUpdateTimestamp { get; set; }

        /// <summary>
        /// Datum und Uhrzeit wann der Eintrag angelegt wurde
        /// </summary>
        public virtual DateTime? CreationTimestamp { get; set; } = DateTime.Now;
    }

    public enum AccessDataTypeEnum
    {
        User = 1,
        Admin = 2,
        AdminUser = 3
    }
}
