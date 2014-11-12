namespace VirtualPiano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VirtualPiano.Data.Common.Models;

    public class Teacher : AuditInfo, IDeletableEntity
    {
        private ICollection<ApplicationUser> students;

        public Teacher()
        {
            this.students = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int YearOfexperience { get; set; }

        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 50)]
        public string AboutMe { get; set; }

        public RequestStatus Status { get; set; }

        public virtual Rating Rating { get; set; }

        public virtual ICollection<ApplicationUser> Students 
        {
            get { return this.students; }
            set { this.students = value; }
        }


        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
