using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilbaLeaf.Entities
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(16,4)")]
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ValidityDay { get; set; }
        [Column(TypeName = "decimal(16,4)")]
        public decimal RenewalCharge { get; set; }
        public string IsDefault { get; set; }
    }
}