namespace EFSettings.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Setting.Settings")]
    public partial class Settings
    {
        [Key]
        public int IDSettings { get; set; }

        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        [Required]
        [StringLength(500)]
        public string Value { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int IDTypeValue { get; set; }

        public int IDService { get; set; }
    }
}
