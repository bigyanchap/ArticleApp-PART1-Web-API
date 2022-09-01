using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BilbaLeaf.DTO
{
    public class ArticleDTO
    {
        public Int64 Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Status { get; set; }
        public string Status_ { get; set; }
        public int Season { get; set; }//Refer Season in EnumsLikeObjects.cs
        public int TwentyFourHourTiming { get; set; } // Refer TwentyFourHourTiming in EnumsLikeObjects.cs
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public DateTime LastPublished { get; set; }
    }

    public class ArticleImageDTO
    {
        public Int64 Id { get; set; }
        public Int64 ArticleId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public int OrderBy { get; set; }
    }

    /*SCRIPTURAL REFERENCE aka SHASTRA PRAMANA and SCIENTIFIC RESEARCH PAPERS*/
    public class ArticleReferenceDTO
    {
        public Int64 Id { get; set; }
        [Required]
        public Int64 ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
    public class ArticleReferenceImageUrlDTO
    {
        public Int64 Id { get; set; }
        public string ImageUrl { get; set; }
    }
    public class ArticleReferenceImagePathDTO
    {
        public Int64 Id { get; set; }
        public string ImagePath { get; set; }
    }
    public class ArticleQueryObject : QueryObject
    {
        public string SearchString { get; set; }
        public int Status { get; set; }
        public int TwentyFourHourTiming { get; set; }
        public int Season { get; set; }
        public int PublishDateEnumSelectedOption { get; set; }
        public DateTime PublishDate_From { get; set; }
        public DateTime PublishDate_To { get; set; }
    }
    public class ArticleMinified
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
}
