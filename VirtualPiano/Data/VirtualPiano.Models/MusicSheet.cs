namespace VirtualPiano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VirtualPiano.Data.Common.Models;
    using VirtualPiano.Web.Infrastructure.Mapping;

    public class MusicSheet : AuditInfo, IDeletableEntity, IMapFrom<MusicSheet>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public string Notes { get; set; }

        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public int CategoryId { get; set; }

        public virtual MusicSheetsCategory Category { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
