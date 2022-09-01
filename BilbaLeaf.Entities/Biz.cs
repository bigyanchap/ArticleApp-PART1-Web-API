using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BilbaLeaf.Entities
{
    /*Biz means Third Party Business or Organization*/
    public class Biz
    {
        [Key]
        public Int64 Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [Required]
        [StringLength(50)]
        public string BizName { get; set; }
        public string KeyPersonName { get; set; }
        public string Designation { get; set; }
        public string BizDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        /*Image that represents the Business: */
        public string ImageName { get; set; } 
        public string ImagePath { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsAcceptTerm { get; set; }
        /*Account Not Deleted (Deactivated=Soft Delete)*/
        public bool IsActivated { get; set; }
        public virtual BizAddress BizAddress { get; set; }
    }
}