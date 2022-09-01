    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BilbaLeaf.Entities
{
    public class ArticleKeyword
    {
        [Key]
        public Int64 Id { get; set; }
        [ForeignKey("ArticleId")]
        public Int64 ArticleId { get; set; }
        [ForeignKey("KeywordId")]
        public Int64 KeywordId { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
