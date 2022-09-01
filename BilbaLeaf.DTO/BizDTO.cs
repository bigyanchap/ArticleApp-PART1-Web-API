using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BilbaLeaf.DTO
{
    /*Biz means Third Party Business*/
    public class BizDTO
    {
        public Int64 Id { get; set; }
        public string AppUserId { get; set; }
        public int SubscriptionId { get; set; }
        public string BizName { get; set; }
        public string KeyPersonName { get; set; }
        public string Designation { get; set; }
        public string BizDescription { get; set; }

        /*Featured during premium paid duration: */
        public bool IsFeatured { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        /*1200px x 600px Banner Image that represents the Business: */
        public string ImageName { get; set; } 
        public string ImagePath { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAcceptTerm { get; set; }

        /*Email Params: */
        public string Body { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
    }
}