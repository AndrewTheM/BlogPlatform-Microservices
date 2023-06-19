# Blog Platform "The Hivemind"

**Distributed .NET Application**

Based on microservice architecture.
Containerized with Docker Compose.
Uses IdentityServer4 as identity provider.
Integrates Azure Cognitive Services for AI functionality.

To run, execute the following commands with Docker CLI:

```
docker compose build
```

```
docker compose up -d
```

Core features:
- post publishing
- comment thread
- post tagging
- post ratings
- automoderation with AI
- post TTS

Project structure:

Services
 1. Posts
    - API
    - BusinessLogic
    - DataAccess
 2. Comments
    - API
    - BusinessLogic
    - DataAccess
 3. Accounts
    - API
    - Infrastructure
    - Application
    - Domain
 4. Files
    - API
 5. Identity
    - IdentityServer
 6. Intelligence
    - API

Gateway
1. Aggregator
2. ApiGateway

Presentation
1. UI
