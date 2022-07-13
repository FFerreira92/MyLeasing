using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyLeasing.Web.Data.Entities
{
    public class Lessee : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Document*")]
        public int Document { get; set; }

        [Required]
        [Display(Name = "First Name*")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name*")]
        public string LastName { get; set; }

        [Display(Name = "Fixed Phone")]
        public long? FixedPhone { get; set; }


        [Display(Name = "Cell Phone")]
        public long? CellPhone { get; set; }


        public string Address { get; set; }

        public string Photo { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public User User { get; set; }
    }
}
