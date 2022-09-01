using AutoMapper;
using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using BilbaLeaf.Repository;
using BilbaLeaf.Repository.Infrastructure;
using BilbaLeaf.Service.Extension;
using BilbaLeaf.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static BilbaLeaf.Entities.Enums;

namespace BilbaLeaf.Service
{
    public class KeywordService:IKeywordService
    {
        IKeywordRepository _keywordRepository;
        ISynonymRepository _synonymRepository;
        IArticleKeywordRepository _articleKeywordRepository;
        IMapper _mapper;
        IUnitOfWork _unitOfWork;
        public KeywordService(
            IKeywordRepository keywordRepository,
            IArticleKeywordRepository articleKeywordRepository,
            ISynonymRepository synonymRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _articleKeywordRepository = articleKeywordRepository;
            _keywordRepository = keywordRepository;
            _synonymRepository = synonymRepository;
            _mapper = mapper;
            _unitOfWork =unitOfWork;
        }
        #region Synonym
        public SynonymDTO GetSynonymById(Int64 id)
        {
            var result =_synonymRepository.GetById(id);
            return _mapper.Map<SynonymDTO>(result);
        }
        public IEnumerable<SynonymModifiedDTO> GetManySynonym(Int64 KeywordId)
        {
            var synonyms = _synonymRepository.FindBy(s => s.KeywordId == KeywordId);
            var result = synonyms.Select(s=> new SynonymModifiedDTO()
            {
                Id=s.Id,
                KeywordId =s.KeywordId,
                Name=s.Name,
                Language= ((Language)(s.Language)).ToString()
            });
            return result;
        }
        public async Task<Int64> CreateSynonym(SynonymDTO model)
        {
            Synonym obj = _mapper.Map<Synonym>(model);
            await _synonymRepository.Add(obj);
            await _unitOfWork.Commit();
            return obj.Id;
        }
        public async Task<Int64> UpdateSynonym(SynonymDTO model)
        {

            var synonym = await _synonymRepository.GetSingle(model.Id);
            if (synonym != null)
            {

                synonym.KeywordId= model.KeywordId;
                synonym.Name = model.Name;
                synonym.Language = model.Language;
                await _unitOfWork.Commit();
            }
            return synonym.Id;
        }
        public async Task DeleteSynonym(Int64 id)
        {
            var synonym = await _synonymRepository.GetSingle(id);
            _synonymRepository.Delete(synonym);
            await _unitOfWork.Commit();
        }

        #endregion Synonym

        #region Keyword
        public bool CheckKeywordName(KeywordDTO keywordDTO)
        {
            if (keywordDTO.Id > 0)
            {
                var check = _keywordRepository.FindBy(x => x.Name.ToLower().Trim() == keywordDTO.Name.ToLower().Trim() && x.Id != keywordDTO.Id);
                return check.Any();
            }
            else
            {
                var check = _keywordRepository.FindBy(x => x.Name.ToLower().Trim() == keywordDTO.Name.ToLower().Trim());
                return check.Any();
            }
        }
        public async Task<QueryResult<KeywordDTO>> GetAllPaged(QueryObject query)
        {
            if (string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "Id";
            }
            var columnMap = new Dictionary<string, Expression<Func<Keyword, object>>>()
            {
                ["Id"] = x => x.Id,
                ["Name"] = x => x.Name,
                ["Description"] = x => x.Description
            };
            var keywords = _keywordRepository.GetAll();

            if (!string.IsNullOrEmpty(query.SearchString))
            {
                keywords = keywords.Where(a => a.Name.Trim().ToLower().Contains(query.SearchString.Trim().ToLower())
                                            || a.Description.Trim().ToLower().Contains(query.SearchString.Trim().ToLower()));
            }
            
            var pagedItems = keywords.ApplyOrdering(query, columnMap);

            var resultant = pagedItems.Select(kw => new KeywordDTO
            {
                Id = kw.Id,
                Name = kw.Name,
                Description = kw.Description
            });
            var queryResult = new QueryResult<KeywordDTO>
            {
                TotalItems = keywords.Count(),
                Items = resultant
            };
            return queryResult;
        }
        public async Task<Int64> Create(KeywordDTO model)
        {
            if(model.Name.Any(_char => Char.IsWhiteSpace(_char)))
            {
                return -1;
            }
            var keyword = new KeywordDTO()
            {
                Id=model.Id,
                Name=model.Name.ToLower(),
                Description=model.Description
            };
            Keyword _keyword =_mapper.Map<Keyword>(keyword);
            await _keywordRepository.Add(_keyword);
            await _unitOfWork.Commit();
            return _keyword.Id;
        }
        public async Task<Int64> Update(KeywordDTO model)
        {
            if (model.Name.Any(_char => Char.IsWhiteSpace(_char)))
            {
                return -1;
            }
            var keyword = await _keywordRepository.GetSingle(model.Id);
            if (keyword != null)
            {
                keyword.Name = model.Name.ToLower();
                keyword.Description = model.Description;
                await _unitOfWork.Commit();
            }
            return keyword.Id;
        }
        public async Task Delete(Int64 id)
        {
            var keyword =await _keywordRepository.GetSingle(id);
            _keywordRepository.Delete(keyword);
            await _unitOfWork.Commit();
        }

        public async Task<KeywordDTO> GetKeywordById(Int64 id)
        {
            var keyword = await _keywordRepository.GetSingle(id);
            return _mapper.Map<KeywordDTO>(keyword);
        }
        #endregion keyword

        #region Article
        public List<DisplayValueDTO> GetKeywordsBySubStr(string word)
        {
            var keywords = (from kw in _keywordRepository.GetAll()
                            where kw.Name.StartsWith(word)
                            select new DisplayValueDTO
                            {
                                Display = kw.Name,
                                Value = kw.Id
                            }).ToList();
            return keywords;
        }
        
        public async Task<bool> SaveKeywords(KeywordBundle keywordBundle)
        {
            try
            {
                var _articleKeywords = from articleKeyword in _articleKeywordRepository.GetAll()
                                where articleKeyword.ArticleId == keywordBundle.ArticleId
                                select articleKeyword;

                foreach (var articleKeyword in _articleKeywords)
                {
                    DisplayValueDTO incomingAkw = keywordBundle.DisplayValues.FirstOrDefault(x=>x.Value==articleKeyword.KeywordId);
                    if (incomingAkw == null)
                    {
                        _articleKeywordRepository.Delete(articleKeyword);
                    }
                }
                
                foreach (var keyWord in keywordBundle.DisplayValues)
                {
                    var articleKeyword = _articleKeywordRepository.GetById(x => x.KeywordId == keyWord.Value && x.ArticleId == keywordBundle.ArticleId);
                    if (articleKeyword == null)
                    {
                        ArticleKeyword articleKeyword_ = new ArticleKeyword()
                        {
                            ArticleId = keywordBundle.ArticleId,
                            KeywordId = keyWord.Value
                        };
                        await _articleKeywordRepository.Add(articleKeyword_);
                    }
                }
                await this._unitOfWork.Commit();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        
        public List<DisplayValueDTO> GetKeywordsByArticleId(Int64 articleId)
        {
            
            var keywords = (from articleKeyword in _articleKeywordRepository.GetAll()
                            join keyword in _keywordRepository.GetAll() on articleKeyword.KeywordId equals keyword.Id
                            where articleKeyword.ArticleId == articleId
                            select new DisplayValueDTO
                            {
                                Display = keyword.Name,
                                Value = articleKeyword.KeywordId
                            }).ToList();
            return keywords;
        }
        #endregion Article

        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }
    }
}
