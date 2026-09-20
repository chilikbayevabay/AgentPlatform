using A2A;
using A2A.AspNetCore;
using ServiceDefaults;
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
var card = new AgentCard { Name = "Orchestrator", Description = "Coordinates ProfileAgent, DocumentAgent, and SearchMcpServer.", Version = "1.0.0", SupportedInterfaces = [new AgentInterface { Url = "/a2a", ProtocolBinding = "HTTP+JSON", ProtocolVersion = "1.0" }], DefaultInputModes = ["text/plain"], DefaultOutputModes = ["text/plain"] };
builder.Services.AddA2AAgent<OrchestratorHandler>(card);
builder.Services.AddHttpClient("agents");
var app = builder.Build();
app.MapDefaultEndpoints();
app.MapA2A("/a2a");
app.MapGet("/.well-known/agent-card.json", () => Results.Ok(card));
app.MapGet("/", () => Results.Ok(new { service = "Orchestrator", orchestration = "A2A over ProfileAgent and DocumentAgent; MCP search stub" }));
app.Run();
public sealed class OrchestratorHandler(IHttpClientFactory clients) : IAgentHandler
{
    public async Task ExecuteAsync(RequestContext context, AgentEventQueue queue, CancellationToken cancellationToken)
    {
        var profile = Environment.GetEnvironmentVariable("PROFILE_AGENT_URL") ?? "http://localhost:5101";
        var document = Environment.GetEnvironmentVariable("DOCUMENT_AGENT_URL") ?? "http://localhost:5102";
        var search = Environment.GetEnvironmentVariable("SEARCH_MCP_SERVER_URL") ?? "http://localhost:5103";
        using var client = clients.CreateClient("agents");
        var profileCard = await client.GetStringAsync($"{profile}/.well-known/agent-card.json", cancellationToken);
        var documentCard = await client.GetStringAsync($"{document}/.well-known/agent-card.json", cancellationToken);
        var searchService = await client.GetStringAsync(search, cancellationToken);
        await new MessageResponder(queue, context.ContextId).ReplyAsync(
            $"Orchestration stub ready. ProfileAgent card: {profileCard.Length} bytes; " +
            $"DocumentAgent card: {documentCard.Length} bytes; MCP search service: {searchService.Length} bytes.",
            cancellationToken);
    }
    public Task CancelAsync(RequestContext context, AgentEventQueue queue, CancellationToken cancellationToken) => Task.CompletedTask;
}
