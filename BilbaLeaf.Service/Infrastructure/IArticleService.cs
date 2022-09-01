using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BilbaLeaf.Service.Infrastructure
{
    public interface IArticleService
    {
        #region basic
        ArticleDTO GetArticleById(Int64 id);
        Task<QueryResult<ArticleDTO>> GetAllPaged(ArticleQueryObject query);
        Task<Int64> Create(ArticleDTO model);
        Task<Int64> Update(ArticleDTO model);
        Task Delete(ArticleDTO model);
        #endregion basic

        #region article-images
        ArticleImageDTO GetArticleImage(Int64 Id);
        bool IsExistingCoverImage(string imagePath);
        Task<Int64> UpdateImage(ArticleDTO model);
        #endregion article-images


        void SaveChanges();

        #region article-reference
        long DeleteRef(Int64 id);
        Task UploadRefImage(Int64 referenceId, string filepath);
        ArticleReferenceImageUrlDTO GetArticleReferenceImage(Int64 referenceId);
        ArticleReferenceImagePathDTO GetArticleReferenceImagePath(Int64 referenceId);
        string GetRefImageName(Int64 Id);
        long CreateRef(ArticleReferenceDTO model);
        long UpdateRef(ArticleReferenceDTO model);
        ArticleReferenceDTO GetReferenceById(Int64 id);
        List<ArticleReferenceDTO> GetReferencesByArticleId(Int64 articleId);

        #endregion article-reference

        #region web
        List<ArticleMinified> GetForWeb();
        #endregion web
    }
}