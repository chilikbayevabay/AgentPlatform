using ModelContextProtocol.Server;
using System.ComponentModel;
namespace SearchMcpServer;
[McpServerToolType]
public sealed class SearchTools
{
    [McpServerTool, Description("Searches the Qdrant index (stub; no network call is made).")]
    public static string Search([Description("The search query")] string query) => $"Qdrant search stub: no results for '{query}'.";
}
