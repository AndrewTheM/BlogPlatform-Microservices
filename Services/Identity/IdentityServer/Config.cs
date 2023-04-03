// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResource(
                "roles",
                "Your role(s)",
                userClaims: new[] { "role" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("postsApi", "Posts API"),
            new ApiScope("commentsApi", "Comments API"),
            new ApiScope("accountsApi", "Accounts API"),
            new ApiScope("filesApi", "Files API")
        };

    public static IEnumerable<Client> GetClients(string clientUrl) =>
        new Client[]
        {
            new Client
            {
                ClientId = "api-client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowOfflineAccess = true,
                AllowedScopes = { "postsApi", "commentsApi", "accountsApi", "filesApi" }
            },
            new Client
            {
                ClientId = "ui-client",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { $"{clientUrl}/signin-oidc" },
                PostLogoutRedirectUris = { $"{clientUrl}/signout-callback-oidc" },

                //AllowAccessTokensViaBrowser = false,
                AllowOfflineAccess = true,
                //AlwaysIncludeUserClaimsInIdToken = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    "roles",
                    "postsApi",
                    "commentsApi",
                    "accountsApi",
                    "filesApi"
                }
            }
        };
}
