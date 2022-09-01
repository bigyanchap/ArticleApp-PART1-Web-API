using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.DTO
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }

    }
    public class QueryResultRating<T>
    {
        public int TotalItems { get; set; }
        public T Items { get; set; }

    }

    public class QueryResultSingle<T>
    {
        public int TotalItems { get; set; }
        public T Item { get; set; }
    }
}
