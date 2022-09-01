using System;
using System.Collections.Generic;
using System.Text;

namespace BilbaLeaf.Entities
{
    public class BaseEntity
    {
        public Int64? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Int64? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
