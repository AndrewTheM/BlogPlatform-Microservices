﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
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
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource(
                "roles",
                "Your role(s)",
                userClaims: new[] { "role" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("posts", "Posts"),
            new ApiScope("comments", "Comments"),
            new ApiScope("accounts", "Accounts"),
            new ApiScope("files", "Files"),
            new ApiScope("intelligence", "Intelligence"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource
            {
                Name = "postsApi",
                DisplayName = "Posts API",
                Scopes = new[] { "posts" }
            },
            new ApiResource
            {
                Name = "commentsApi",
                DisplayName = "Comments API",
                Scopes = new[] { "comments" }
            },
            new ApiResource
            {
                Name = "accountsApi",
                DisplayName = "Accounts API",
                Scopes = new[] { "accounts" }
            },
            new ApiResource
            {
                Name = "filesApi",
                DisplayName = "Files API",
                Scopes = new[] { "files" }
            },
            new ApiResource
            {
                Name = "intelligenceApi",
                DisplayName = "Intelligence API",
                Scopes = new[] { "intelligence" }
            },
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
                AllowedScopes = { "posts", "comments", "accounts", "files", "intelligence" }
            },
            new Client
            {
                ClientId = "ui-client",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { $"{clientUrl}/signin-oidc" },
                PostLogoutRedirectUris = { $"{clientUrl}/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    "posts",
                    "comments",
                    "accounts",
                    "files",
                    "intelligence"
                }
            }
        };
}
