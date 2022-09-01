
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilbaLeaf.Repository.Common
{
    public interface IDbFactory : IDisposable
    {
        BilbaLeafContext Init();
    }
}
