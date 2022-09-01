using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BilbaLeaf.Entities.Identity
{
    public class AppUser :  IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        //public virtual ICollection<Biz> Businesses { get; set; }
    }
}