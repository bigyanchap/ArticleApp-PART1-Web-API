using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BilbaLeaf.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecretController : ControllerBase
    {
        [Authorize]
        public string Index()
        {
            return "secret mesasge from APIOne";
        }

        [Route("Newsecret")]
        [Authorize]
        public object NewSecret()
        {
            return new { msg = "newSecret"};
        }
    }
}