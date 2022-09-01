using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controller
{
    public class IdentityController : Microsoft.AspNetCore.Mvc.Controller
    {
        public ActionResult login()
        {
            return View();
        }
    }
}