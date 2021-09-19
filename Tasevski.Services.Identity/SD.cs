using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace Tasevski.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        //Ova e za Admin
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("tasevski", "Tasevski Server"),
                new ApiScope(name: "read", displayName: "Read your data."),
                new ApiScope(name: "write", displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        //ova e za Client
        public static IEnumerable<Client> Clients =>
           new List<Client>
           {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256())}, //vo produkcija kje stavime nekoe poslozeno ime mesto "secret"
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read", "write", "profile"}
                },
                new Client
                {
                    ClientId = "tasevski",
                    ClientSecrets = { new Secret("secret".Sha256())}, //vo produkcija kje stavime nekoe poslozeno ime mesto "secret"
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:44302/signin-oidc"}, //oidc = open id connect
                    PostLogoutRedirectUris = { "https://localhost:44302/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "tasevski"
                    }
                },
           };
    }
}