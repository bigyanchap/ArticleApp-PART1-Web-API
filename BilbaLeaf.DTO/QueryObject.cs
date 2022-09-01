using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.DTO
{
    public class QueryObject
    {
        public bool IsSortAsc { get; set; }
        public string SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
    }
}
