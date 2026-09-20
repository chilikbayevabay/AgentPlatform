using A2A;

namespace ProfileAgent;

public sealed class AgentHandler : IAgentHandler
{
    public async Task ExecuteAsync(RequestContext context, AgentEventQueue queue, CancellationToken cancellationToken)
    {
        var responder = new MessageResponder(queue, context.ContextId);
        await responder.ReplyAsync($"Profile accepted: {context.UserText}", cancellationToken);
    }

    public Task CancelAsync(RequestContext context, AgentEventQueue queue, CancellationToken cancellationToken) =>
        Task.CompletedTask;
}
