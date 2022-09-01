using BilbaLeaf.Entities;
using BilbaLeaf.Repository.Common;
using BilbaLeaf.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Repository.Repository
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
