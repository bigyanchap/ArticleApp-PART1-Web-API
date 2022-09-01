using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BilbaLeaf.Entities
{
    public class Category
    {
        public Int64 Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public Int64? ParentId { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public int StatusId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
}
