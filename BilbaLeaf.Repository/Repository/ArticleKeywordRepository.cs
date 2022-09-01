using BilbaLeaf.Entities;
using BilbaLeaf.Repository.Common;
using BilbaLeaf.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Repository.Repository
{
    public class ArticleKeywordRepository : RepositoryBase<ArticleKeyword>, IArticleKeywordRepository
    {
        public ArticleKeywordRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
