using BilbaLeaf.Entities;
using BilbaLeaf.Repository.Common;
using BilbaLeaf.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Repository.Repository
{
    public class KeywordRepository : RepositoryBase<Keyword>, IKeywordRepository
    {
        public KeywordRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
    public class SynonymRepository : RepositoryBase<Synonym>, ISynonymRepository
    {
        public SynonymRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
