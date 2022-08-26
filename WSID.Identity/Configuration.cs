using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSID.Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> { new ApiResource("WSID.Api") };

        public static IEnumerable<Client> GetClietns() =>
            new List<Client> {
                new Client
                {
                    ClientId = "test_client",
                    ClientSecrets = { new Secret("test_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "WSID.Api" },                    
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> {
                new ApiScope(name: "WSID.Api")
            };
    }
}
