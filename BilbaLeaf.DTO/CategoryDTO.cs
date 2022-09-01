using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BilbaLeaf.DTO
{
    public class CategoryDTO
    {
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Int64? ParentId { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public int StatusId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
}
