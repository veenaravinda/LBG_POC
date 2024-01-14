using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]     
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        // public string Company { get; set; }

       
        [Column(TypeName = "decimal(18,2)")]
        public decimal Income { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CreditScore { get; set; }

    }
}
