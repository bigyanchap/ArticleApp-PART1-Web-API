using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BilbaLeaf.Entities
{
    /*Third Party Business and Subscription Combining Table*/
    public class BizSubsHistory
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 BizId { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public DateTime LastRenewalDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        [Column(TypeName = "decimal(16,4)")]
        public decimal PaidAmount { get; set; }
    }
}