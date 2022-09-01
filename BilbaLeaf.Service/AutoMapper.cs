using AutoMapper;
using BilbaLeaf.DTO;
using BilbaLeaf.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Service
{
    public static class AutoMapping
    {
        public static IMapper Automapperinitializer()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArticleDTO, Article>().ReverseMap();
                cfg.CreateMap<ArticleReferenceDTO, ArticleReference>().ReverseMap();
                cfg.CreateMap<ArticleImageDTO, ArticleImage>().ReverseMap();
                cfg.CreateMap<CategoryDTO, Category>().ReverseMap();
                cfg.CreateMap<KeywordDTO, Keyword>().ReverseMap();
                cfg.CreateMap<SynonymDTO, Synonym>().ReverseMap();
                cfg.CreateMap<CountryDTO, Country>().ReverseMap();
                cfg.CreateMap<BizDTO, Biz>().ReverseMap();
            });
            return config.CreateMapper();
        }

        private static void CreateMap<T1, T2>()
        {
            throw new NotImplementedException();
        }
    }
}
