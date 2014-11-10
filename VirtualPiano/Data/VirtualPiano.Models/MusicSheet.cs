namespace VirtualPiano.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VirtualPiano.Data.Common.Models;

    public class MusicSheet : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
