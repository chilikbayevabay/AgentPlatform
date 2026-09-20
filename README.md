# Agent Platform

Минимальный .NET 10 workspace из четырёх HTTP-сервисов:

- **ProfileAgent** — A2A agent, принимает профиль-заглушку.
- **DocumentAgent** — A2A agent, возвращает результат PDF extraction stub.
- **SearchMcpServer** — официальный `ModelContextProtocol.AspNetCore`, streamable HTTP MCP tool-заглушка поиска Qdrant.
- **Orchestrator** — A2A endpoint; при сообщении проверяет ProfileAgent и DocumentAgent по A2A discovery и обращается к MCP search service.

Все A2A сервисы публикуют `/.well-known/agent-card.json` и `/a2a`, а каждый сервис — `/health` и `/alive`. Реальных PDF, Qdrant и LLM нет. `AppHost` связывает оркестратор с тремя downstream-проектами через service discovery; при ручном запуске URL можно настроить переменными `PROFILE_AGENT_URL`, `DOCUMENT_AGENT_URL` и `SEARCH_MCP_SERVER_URL`.

## Проверка

```powershell
dotnet restore AgentPlatform.slnx
dotnet build AgentPlatform.slnx
dotnet test AgentPlatform.slnx
dotnet run --project src\AppHost
```
