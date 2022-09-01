using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly IIdentityServerInteractionService _identity;
        public HomeController(IIdentityServerInteractionService identity)
        {
            _identity = identity;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Error(string errorId)
        {
            @ViewBag.Error = errorId;
            var errormessage = await _identity.GetErrorContextAsync(errorId);
            return View();
        }
    }
}