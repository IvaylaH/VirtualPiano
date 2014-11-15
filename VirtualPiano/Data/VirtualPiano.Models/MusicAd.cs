namespace VirtualPiano.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VirtualPiano.Data.Common.Models;

    public class MusicAd : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 30)]
        public string Content { get; set; }

        [Required]
        public RequestStatus Status { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int CategoryId { get; set; }

        public virtual AdCategory Category { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
