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
    public class ArticleService : IArticleService
    {
        IArticleRepository _articleRepository;
        IArticleKeywordRepository _articleKeywordRepository;
        IArticleReferenceRepository _articleReferenceRepository;
        IArticleImageRepository _articleImageRepository;
        ICommonService _commonService;
        IMapper _mapper;
        IUnitOfWork _unitOfWork;
        public ArticleService(
            IArticleRepository articleRepository,
            IArticleKeywordRepository articleKeywordRepository,
            IArticleReferenceRepository articleReferenceRepository,
            IArticleImageRepository articleImageRepository,
            ICommonService commonService,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _articleRepository = articleRepository;
            _articleImageRepository = articleImageRepository;
            _articleKeywordRepository = articleKeywordRepository;
            _articleReferenceRepository = articleReferenceRepository;
            _commonService = commonService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region basic
        public ArticleDTO GetArticleById(Int64 id)
        {
            var article = _articleRepository.GetById(id);
            return _mapper.Map<ArticleDTO>(article);

        }
        public async Task<QueryResult<ArticleDTO>> GetAllPaged(ArticleQueryObject query)
        {
            if (string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "Id";
            }
            var columnMap = new Dictionary<string, Expression<Func<Article, object>>>()
            {
                ["Id"] = x => x.Id,
                ["Title"] = x => x.Title
            };
            var articles = _articleRepository.GetAll();

            if (!string.IsNullOrEmpty(query.SearchString))
            {
                articles = articles.Where(a => a.Title.Trim().ToLower().Contains(query.SearchString.Trim().ToLower())
                                            || a.ShortDescription.Trim().ToLower().Contains(query.SearchString.Trim().ToLower())
                                          );
            }
            //DATE FILTER:
            if (query.PublishDateEnumSelectedOption != (int)Enums.DatingEnum.anytime)
            {
                Tuple<DateTime?, DateTime?> tupleStartDate = _commonService.getDateRange(query.PublishDateEnumSelectedOption, query.PublishDate_From, query.PublishDate_To);

                articles = articles.Where(date => ((tupleStartDate.Item1.HasValue == false || date.LastPublished >= tupleStartDate.Item1) && (
                             tupleStartDate.Item2.HasValue == false || date.LastPublished < tupleStartDate.Item2)));
            }

            switch (query.Status)
            {
                case (int)ArticleStatus.DRAFT:
                    articles = articles.Where(x => x.Status == (int)Enums.ArticleStatus.DRAFT);
                    break;
                case (int)ArticleStatus.PUBLISHED:
                    articles = articles.Where(x => x.Status == (int)Enums.ArticleStatus.PUBLISHED);
                    break;
            }
            switch (query.Season)
            {
                case (int)SeasonEnum.any:
                    articles = articles.Where(x => x.Season == (int)SeasonEnum.any);
                    break;
                case (int)SeasonEnum.basanta:
                    articles = articles.Where(x => x.Season == (int)SeasonEnum.basanta);
                    break;
                case (int)SeasonEnum.grishma:
                    articles = articles.Where(x => x.Season == (int)SeasonEnum.grishma);
                    break;
                case (int)SeasonEnum.barsha:
                    articles = articles.Where(x => x.Season == (int)SeasonEnum.barsha);
                    break;
                case (int)SeasonEnum.sharad:
                    articles = articles.Where(x => x.Season == (int)SeasonEnum.sharad);
                    break;
                case (int)SeasonEnum.hemanta:
                    articles = articles.Where(x => x.Season == (int)SeasonEnum.hemanta);
                    break;
                case (int)SeasonEnum.shishir:
                    articles = articles.Where(x => x.Season == (int)SeasonEnum.shishir);
                    break;
            }
            switch (query.TwentyFourHourTiming)
            {
                case (int)TwentyFourHourTimingEnum.any:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.any);
                    break;
                case (int)TwentyFourHourTimingEnum.brahmaMuhurta:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.brahmaMuhurta);
                    break;
                case (int)TwentyFourHourTimingEnum.dawn:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.dawn);
                    break;
                case (int)TwentyFourHourTimingEnum.morning:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.morning);
                    break;
                case (int)TwentyFourHourTimingEnum.afternoon:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.afternoon);
                    break;
                case (int)TwentyFourHourTimingEnum.dusk:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.dusk);
                    break;
                case (int)TwentyFourHourTimingEnum.evening:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.evening);
                    break;
                case (int)TwentyFourHourTimingEnum.timeToSleep:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.timeToSleep);
                    break;
                case (int)TwentyFourHourTimingEnum.night:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.night);
                    break;
                case (int)TwentyFourHourTimingEnum.wholeMorning:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.wholeMorning);
                    break;
                case (int)TwentyFourHourTimingEnum.wholeDay:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.wholeDay);
                    break;
                case (int)TwentyFourHourTimingEnum.wholeEvening:
                    articles = articles.Where(x => x.TwentyFourHourTiming == (int)TwentyFourHourTimingEnum.wholeEvening);
                    break;
            }
            var pagedItems = articles.ApplyOrdering(query, columnMap);
            var resultant = pagedItems.Select(article => new ArticleDTO
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.ShortDescription.Length < 50 ? article.ShortDescription : article.ShortDescription.Substring(0, 50) + "...",
                LastPublished = article.LastPublished == null ? new DateTime() : (DateTime)article.LastPublished,
                ImagePath = article.ImagePath,
                ImageName = article.ImageName,
                Status = article.Status,

                Status_ = ((ArticleStatus)article.Status).ToString()
            });
            var queryResult = new QueryResult<ArticleDTO>
            {
                TotalItems = articles.Count(),
                Items = resultant
            };
            return queryResult;
        }

        public async Task<Int64> Create(ArticleDTO model)
        {
            Article obj = _mapper.Map<Article>(model);
            obj.Status = (int)Enums.ArticleStatus.DRAFT;
            obj.Season = Season.SeasonList.FirstOrDefault(x => x.Name == "any").SeasonId;
            obj.TwentyFourHourTiming = TwentyFourHourTiming.TwentyFourHourTimingList.FirstOrDefault(x => x.Name == "any").TwentyFourHourTimingId;
            obj.ImageName = String.Empty;
            obj.ImagePath = String.Empty;
            await _articleRepository.Add(obj);
            await _unitOfWork.Commit();
            return obj.Id;
        }
        public async Task<Int64> Update(ArticleDTO model)
        {

            var obj = await _articleRepository.GetSingle(model.Id);
            if (obj != null)
            {
                obj.Title = model.Title;
                obj.Status = model.Status;
                obj.ShortDescription = model.ShortDescription;
                obj.Description = model.Description;
                obj.Season = model.Season;
                obj.TwentyFourHourTiming = model.TwentyFourHourTiming;
                await _unitOfWork.Commit();
            }
            return obj.Id;
        }
        /*UNUSED:*/
        public async Task Delete(ArticleDTO model)
        {
            var article = await _articleRepository.GetSingle(model.Id);
            _articleRepository.Delete(article);
            await _unitOfWork.Commit();
        }
        #endregion basic

        #region article-image
        public ArticleImageDTO GetArticleImage(Int64 Id)
        {
            var articleImage = _articleImageRepository.GetById(x => x.Id == Id);
            var _articleImage = _mapper.Map<ArticleImageDTO>(articleImage);
            return _articleImage;
        }
        public bool IsExistingCoverImage(string imagePath) =>
            _articleRepository.FindBy(x => x.ImagePath == imagePath).Count()>0;
        public async Task<Int64> UpdateImage(ArticleDTO model)
        {

            var obj = await _articleRepository.GetSingle(model.Id);
            if (obj != null)
            {
                obj.Title = model.Title;
                obj.Status = model.Status;
                obj.ShortDescription = model.ShortDescription;
                obj.Description = model.Description;
                obj.Season = model.Season;
                obj.TwentyFourHourTiming = model.TwentyFourHourTiming;
                obj.ImageName = model.ImageName;
                obj.ImagePath = model.ImagePath;
                await _unitOfWork.Commit();
            }
            return obj.Id;
        }
        #endregion article-image

        #region privateMethods
        public async Task UploadRefImage(Int64 referenceId, string filepath)
        {
            var reference = await _articleReferenceRepository.GetSingle(referenceId);
            reference.ImagePath = filepath;
            await _unitOfWork.Commit();
        }
        public ArticleReferenceImageUrlDTO GetArticleReferenceImage(Int64 referenceId)
        {
            var refer = _articleReferenceRepository.GetById(x => x.Id == referenceId);
            if (refer != null)
            {
                var _ref = new ArticleReferenceImageUrlDTO()
                {
                    Id = referenceId,
                    ImageUrl = refer.ImagePath == null ? null : "Uploads\\ReferenceImages" + "\\" + refer.ImagePath
                };
                return _ref;
            }
            return new ArticleReferenceImageUrlDTO();
        }
        public ArticleReferenceImagePathDTO GetArticleReferenceImagePath(Int64 referenceId)
        {
            var refer = _articleReferenceRepository.GetById(x => x.Id == referenceId);
            var _ref = new ArticleReferenceImagePathDTO()
            {
                Id = referenceId,
                ImagePath = refer.ImagePath
            };
            return _ref;
        }

        public string GetRefImageName(Int64 Id)
        {
            return _articleReferenceRepository.GetById(x => x.Id == Id).ImagePath;
        }
        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }

        #endregion privateMethods

        #region article-reference
        public long DeleteRef(Int64 id)
        {
            var obj = _articleReferenceRepository.GetById(x => x.Id == id);
            _articleReferenceRepository.Delete(obj);
            _unitOfWork.Commit();
            return obj.Id;
        }
        public long CreateRef(ArticleReferenceDTO model)
        {
            ArticleReference obj = _mapper.Map<ArticleReference>(model);
            _articleReferenceRepository.Add(obj);
            _unitOfWork.Commit();
            return obj.Id;
        }
        public long UpdateRef(ArticleReferenceDTO model)
        {
            ArticleReference obj = _articleReferenceRepository.GetById(x => x.Id == model.Id);
            if (obj != null)
            {
                obj.Title = model.Title;
                obj.ArticleId = model.ArticleId;
                obj.Description = model.Description;
                obj.ImagePath = obj.ImagePath;
                _unitOfWork.Commit();
            }
            return obj.Id;
        }
        public ArticleReferenceDTO GetReferenceById(Int64 id)
        {
            var reference = _articleReferenceRepository.GetById(x=>x.Id==id);
            var result = _mapper.Map<ArticleReferenceDTO>(reference);
            return result;
        }

        public List<ArticleReferenceDTO> GetReferencesByArticleId(Int64 articleId)
        {
            var articleReferences = _articleReferenceRepository.FindBy(x => x.ArticleId == articleId);
            var result = _mapper.Map<List<ArticleReferenceDTO>>(articleReferences);
            return result;
        }

        #endregion article-reference

        #region web
        public List<ArticleMinified> GetForWeb()
        {
            var articles = _articleRepository.All;
            List<ArticleMinified> _articles = articles.Select(a => new ArticleMinified
            {
                Id=a.Id,
                ShortDescription=a.ShortDescription,
                Description=a.Description,
                ImageName=a.ImageName,
                ImagePath="Uploads\\ArticleCoverImages\\"+ a.ImagePath,
                Title=a.Title
            }).ToList();

            return _articles;
        }
        #endregion web
    }
}
