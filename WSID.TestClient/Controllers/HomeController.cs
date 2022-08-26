using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace WSID.TestClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //retrieve accec token
            var serverClient = _httpClientFactory.CreateClient();

            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:7291/");

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "test_client",
                ClientSecret = "test_secret",
                Scope = "WSID.Api"
            });

            //Retrieve data
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            //apiClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("https://localhost:7147/Secret");
            var content = await response.Content.ReadAsStringAsync();

            return Ok(new 
            {
                access_token = tokenResponse.AccessToken,
                message = content
            });

        }
    }
}