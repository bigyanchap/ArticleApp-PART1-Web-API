 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BilbaLeaf.Entities
{
    public class Article
    {
        [Key]
        public Int64 Id { get; set; }
        [StringLength(500)]
        [Required]
        public string Title { get; set; }
        public int Season { get; set; }//Refer Season in EnumsLikeObjects.cs
        public int TwentyFourHourTiming { get; set; } // Refer TwentyFourHourTiming in EnumsLikeObjects.cs
        public int Status { get; set; }/*Enum: Published=1, Draft=2*/
        [StringLength(1500)]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        [StringLength(1000)]
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public DateTime? LastPublished { get; set; }
        public virtual ICollection<ArticleKeyword> ArticleKeywords { get; set; }
        public virtual ICollection<ArticleImage> ArticleImages { get; set; }
        public virtual ICollection<ArticleReference> ArticleReferences { get; set; }
    }

    public class ArticleImage
    {
        public Int64 Id { get; set; }
        [ForeignKey("ArticleId")]
        public Int64 ArticleId { get; set; }
        //public virtual Product Product { get; set; }
        [Required]
        [StringLength(1000)]
        public string ImagePath { get; set; }
        [StringLength(500)]
        public string ImageName { get; set; }
        public int OrderBy { get; set; }
    }

    /*SCRIPTURAL REFERENCE aka SHASTRA PRAMANA and SCIENTIFIC RESEARCH PAPERS*/
    public class ArticleReference
    {
        [Key]
        public Int64 Id { get; set; }
        [ForeignKey("ArticleId")]
        public Int64 ArticleId { get; set; }
        [StringLength(1000)]
        public string Title { get; set; }
        [StringLength(5000)]
        public string Description { get; set; }
        [StringLength(100)]
        public string ImagePath { get; set; }
    }
}
