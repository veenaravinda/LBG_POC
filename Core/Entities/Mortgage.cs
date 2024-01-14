using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Mortgage
    {
        public int Id { get; set; }
        [Required]
      
        public int CustomerId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int LoanDuration { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmount { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyPayment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalIntrest { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPayment { get; set; }


        public DateTime CreatedAt { get; set; }
        public Customer CustomerEntitys { get; set; }
        public Property PropertyEntitys { get; set; }
     //   public List<Customer> CustomerEntitys { get; set; }
       // public List<Property> PropertyEntitys { get; set; }
   // }
}


   

}
