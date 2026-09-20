var builder = DistributedApplication.CreateBuilder(args);

var profileAgent = builder.AddProject<Projects.ProfileAgent>("profile-agent");
var documentAgent = builder.AddProject<Projects.DocumentAgent>("document-agent");
var searchMcpServer = builder.AddProject<Projects.SearchMcpServer>("search-mcp-server");

builder.AddProject<Projects.Orchestrator>("orchestrator")
    .WithReference(profileAgent)
    .WithReference(documentAgent)
    .WithReference(searchMcpServer);

builder.Build().Run();
