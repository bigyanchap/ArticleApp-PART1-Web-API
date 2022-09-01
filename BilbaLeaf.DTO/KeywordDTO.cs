using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BilbaLeaf.DTO
{
    public class KeywordDTO
    {
        public Int64 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SynonymDTO> Synonyms { get; set; }
    }
   
    public class DisplayValueDTO
    {
        public Int64 Value { get; set; }
        public string Display { get; set; }
    }
    public class KeywordBundle {
        public Int64 ArticleId { get; set; }
        public List<DisplayValueDTO> DisplayValues { get; set; }
    }
    public class WordVM
    {
        public string Word { get; set; }
    }
    public class SynonymDTO
    {
        public Int64 Id { get; set; }
        public Int64 KeywordId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Language { get; set; }
    }
    public class SynonymModifiedDTO
    {
        public Int64 Id { get; set; }
        public Int64 KeywordId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
    }
}
