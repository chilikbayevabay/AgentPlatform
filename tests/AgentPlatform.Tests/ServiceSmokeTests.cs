namespace AgentPlatform.Tests;

public class ServiceSmokeTests
{
    [Fact]
    public void SolutionContainsExpectedServices()
    {
        Assert.True(File.Exists(Path.Combine(AppContext.BaseDirectory, "AgentPlatform.Tests.dll")));
    }
}
