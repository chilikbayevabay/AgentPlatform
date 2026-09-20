using A2A;
namespace DocumentAgent;
public sealed class AgentHandler : IAgentHandler
{
    public async Task ExecuteAsync(RequestContext context, AgentEventQueue queue, CancellationToken cancellationToken) =>
        await new MessageResponder(queue, context.ContextId).ReplyAsync($"PDF extraction stub completed for profile: {context.UserText}", cancellationToken);
    public Task CancelAsync(RequestContext context, AgentEventQueue queue, CancellationToken cancellationToken) => Task.CompletedTask;
}
