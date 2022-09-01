

using BilbaLeaf.Repository;

namespace BilbaLeaf.Repository.Common
{
    public class DbFactory : Disposable, IDbFactory
    {

        BilbaLeafContext dbContext;
        public DbFactory(BilbaLeafContext db)
        {
            dbContext = db;
        }
       
        public BilbaLeafContext Init()
        {
            return dbContext;
            //return dbContext ?? (dbContext = new BilbaLeafContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }

  
}
