using BilbaLeaf.Repository;
using System.Threading.Tasks;

namespace BilbaLeaf.Repository.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private BilbaLeafContext dbContext;  

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public BilbaLeafContext DbContext
        {
            get 
            {
               
                return  (dbContext = dbFactory.Init());

            }
        }

        public async Task Commit()
        {
            await DbContext.Commit();
        }
    }
}
