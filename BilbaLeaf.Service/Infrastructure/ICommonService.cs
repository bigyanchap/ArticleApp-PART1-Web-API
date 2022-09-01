using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Service.Infrastructure
{
    public interface ICommonService
    {
        Tuple<DateTime?, DateTime?> getDateRange(int DatedOn, DateTime? Fromval, DateTime? Tillval);
    }
}
