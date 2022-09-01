using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BilbaLeaf.Entities
{
    public class Keyword
    {
        [Key]
        public Int64 Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        public virtual ICollection<Synonym> Synonyms { get; set; }
    }
    public class Synonym
    {
        [Key]
        public Int64 Id { get; set; }
        [ForeignKey("KeywordId")]
        public Int64 KeywordId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int Language { get; set; }/*Refer LanguageEnum*/
    }
}
