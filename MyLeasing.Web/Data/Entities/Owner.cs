﻿using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner
    {
        
        public int Id { get; set; }

        [Required]        
        [Display(Name = "Document*")]
        public int Document { get; set; }

        [Required]
        [Display(Name = "First Name*")]
        public string FirstName { get; set; }

        [Required, Display(Name ="Last Name*")]
        public string LastName { get; set; }

        [Display(Name = "Fixed Phone")]
        public long? FixedPhone  { get; set; }

       
        [Display(Name ="Cell Phone")]
        public long? CellPhone { get; set; }

        public string Adress { get; set; }

        [Display(Name ="Owner Name")]
        public string FullName => $"{FirstName} {LastName}";

    }
}