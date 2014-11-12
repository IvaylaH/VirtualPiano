namespace VirtualPiano.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Rating
    {
        private ICollection<Teacher> teachers;

        public Rating()
        {
            this.teachers = new HashSet<Teacher>();
        }

        [Key]
        public int Id { get; set; }

        [Range(minimum: 0, maximum: 100)]
        public int Value { get; set; }

        public virtual ICollection<Teacher> Teachers 
        {
            get { return this.teachers; }
            set { this.teachers = value; }
        }
    }
}
