using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer4.Configuration
{
    public class IdentityConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("myApi")
                {
                    Scopes = new List<string>{ "myApi.read","myApi.write" },
                    ApiSecrets = new List<Secret>{ new Secret("sensetowereventapi".Sha256()) }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("myApi.read"),
                new ApiScope("myApi.write"),
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1144",
                    Username = "sensetoweruser",
                    Password = "sensetowerpwd",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Sense Tower"),
                        new Claim(JwtClaimTypes.GivenName, "Sense"),
                        new Claim(JwtClaimTypes.FamilyName, "Tower"),
                        new Claim(JwtClaimTypes.WebSite, "http://localhost"),
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "sensetower-event-api",
                    ClientName = "Sense Tower Event API",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("sensetowereventapi".Sha256()) },
                    AllowedScopes = { "myApi.read" }
                },
            };
    }
}