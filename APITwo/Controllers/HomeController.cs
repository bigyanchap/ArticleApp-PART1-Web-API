using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;

namespace APITwo.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("/Home")]
        public async Task<IActionResult> Index()
        {

            var serverClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44336/");
            var tokenresponse=await serverClient.RequestClientCredentialsTokenAsync(
                                new ClientCredentialsTokenRequest
                                {
                                    Address=discoveryDocument.TokenEndpoint,
                                    ClientId= "client_id",
                                    ClientSecret= "client_secret",
                                    Scope= "ApiOne",
                                });

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenresponse.AccessToken);
            var response = await apiClient.GetAsync("https://localhost:44388/secret");
            var content = response.Content.ReadAsStringAsync();
            return Ok(new
            {
                access_tokent= tokenresponse.AccessToken,
                message=content,
            });
        }
    }
}