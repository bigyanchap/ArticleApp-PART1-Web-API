using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BilbaLeaf.Service.Infrastructure
{
    public interface IKeywordService
    {
        #region Synonym
        SynonymDTO GetSynonymById(Int64 id);
        IEnumerable<SynonymModifiedDTO> GetManySynonym(Int64 KeywordId);
        Task<Int64> CreateSynonym(SynonymDTO model);
        Task<Int64> UpdateSynonym(SynonymDTO model);
        Task DeleteSynonym(Int64 id);
        #endregion Synonym

        #region keyword
        bool CheckKeywordName(KeywordDTO productDTO);
        Task<QueryResult<KeywordDTO>> GetAllPaged(QueryObject query);
        Task<KeywordDTO> GetKeywordById(Int64 id);
        Task<Int64> Create(KeywordDTO model);
        Task<Int64> Update(KeywordDTO model);
        Task Delete(Int64 id);
        #endregion keyword

        #region Article
        List<DisplayValueDTO> GetKeywordsBySubStr(string word);
        Task<bool> SaveKeywords(KeywordBundle keywordBundle);
        List<DisplayValueDTO> GetKeywordsByArticleId(Int64 articleId);
        #endregion Article

        void SaveChanges();
    }
}