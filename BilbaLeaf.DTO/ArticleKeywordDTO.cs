    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BilbaLeaf.DTO
{
    public class ArticleKeywordDTO
    {
        [Key]
        public Int64 Id { get; set; }
        [ForeignKey("ArticleId")]
        public Int64 ArticleId { get; set; }
        [ForeignKey("KeywordId")]
        public Int64 KeywordId { get; set; }
        //public virtual KeywordDTO Keyword { get; set; }
    }
}
