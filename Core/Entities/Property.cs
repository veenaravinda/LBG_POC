using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class Property
    {
        public int Id { get; set; }
        
        public string Address { get; set; }
        // public string LastName { get; set; }
        [Required]
        
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        // public string Company { get; set; }
        
        public string Zipcode { get; set; }        
        public string PropType { get; set; }
        public int Bedrooms { get; set; }
        public int YearBuilt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MarketValue { get; set; }
        public int size { get; set; }

    }
}


