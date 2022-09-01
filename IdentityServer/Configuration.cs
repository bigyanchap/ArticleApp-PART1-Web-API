using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> { new ApiResource("ApiOne"), new ApiResource("ApiTwo"), new ApiResource("AdminAgular") };

        public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            
        };

        public static IEnumerable<ApiScope> Apis =>
            new List<ApiScope>
            {
                new ApiScope{ Name="ApiOne" },
                new ApiScope{ Name="ApiTwo" },
                new ApiScope{ Name="AdminAgular" }
            };
        public static IEnumerable<Client> GetClients() =>
            new List<Client> {
                new Client
                {

                        ClientId="client_id",ClientName="API",
                        ClientSecrets={ new Secret("client_secret".ToSha256())},
                        AllowedGrantTypes=GrantTypes.ClientCredentials,
                        AllowedScopes={ "ApiOne","client_AngularAdmin"},
                        RequireConsent=false,
                        AllowedCorsOrigins = { "http://localhost:4200" },
                },

                new Client
                {

                        ClientId="client_id_mvc",ClientName="client_id_mvc",
                        ClientSecrets={ new Secret("client_secret_mvc".ToSha256())},
                        AllowedGrantTypes=GrantTypes.Code,
                        RedirectUris={"https://localhost:44328/"},
                        PostLogoutRedirectUris={ "https://localhost:44328/Home/Index"},
                        AllowedScopes={
                            "ApiOne",
                            "ApiTwo",
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,

                    },
                        RequireConsent=false,

                },
                 new Client {
                    ClientId = "client_AngularAdmin",
                    ClientName="client_AngularAdmin",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200/auth/login" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes = {
                         "ApiOne",
                        IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                          IdentityServerConstants.StandardScopes.Email,
                          IdentityServerConstants.StandardScopes.OfflineAccess,
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                },


            };

        //public static List<IdentityResource> GetIdentityResources()
        //{
        //    return new List<IdentityResource>
        //            {
        //                new IdentityResources.OpenId(),
        //                new IdentityResources.Profile(),
                         
        //            };
        //}
    }
}
