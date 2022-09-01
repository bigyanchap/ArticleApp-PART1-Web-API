using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BilbaLeaf.Entities
{
    public class BizAddress : Address
    {
        public Int64 BizId { get; set; }
    }

    public class UserAddress : Address
    {
        public string AppUserId { get; set; }
    }

    public class Address
    {
        [Key]
        public Int64 Id { get; set; }
        public int CountryId { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        [StringLength(50)]
        public string County { get; set; }
        [StringLength(50)]
        public string Zone { get; set; }
        [StringLength(50)]
        public string District { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string Town { get; set; }
        [StringLength(50)]
        public string AddressLine1 { get; set; }
        [StringLength(50)]
        public string AddressLine2 { get; set; }
        [StringLength(10)]
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    
}
